using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using BlazorServerAPI.Repository;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using dotenv.net;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BlazorServerAPI.Utils.Exceptions;
using BlazorServerAPI.Services;
using BlazorServerAPI.Models.Requests;

namespace BlazorServerAPI.Handlers
{
    public class AuthHandler
    {
        private readonly UserRepository _userService;
        private readonly IMailService _mailService;

        public AuthHandler(UserRepository userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        public async Task<IResponse> Register(User user)
        {
            var hashedPassword = new PasswordHasher<object?>().HashPassword(null, user.Password);
            if (hashedPassword == null)
            {
                throw new InvalidPasswordException("PasswordHasher failed, not enough entropy");
            }
            var newUser = new User(user.Email, hashedPassword);
            newUser = await _userService.CreateUser(newUser);
            await _mailService.SendEmailAsync(new ConfirmRegistrationMailRequest(newUser.Email, newUser.Id));
            return new MessageResponse("User created. Confirmation Mail Sent.");
        }

        public async Task<IResponse> Login(User user)
        {
            var result = await _userService.FindUserByEmail(user.Email);
            if (result == null)
            {
                return new ErrorResponse(error: "Invalid credentials");
            }
            var passwordVerificationResult = new PasswordHasher<object?>().VerifyHashedPassword(null, result.Password, user.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return new ErrorResponse(error: "Invalid credentials");
            }
            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                return new LoginResponse(token: generateJwtToken(result));
            }
            throw new InvalidPasswordException(passwordVerificationResult.ToString());
        }

        public async Task<IResponse> Confirm(string userId)
        {
            var user = await _userService.ConfirmUser(userId);
            if (user == null)
            {
                return new ErrorResponse(error: "Invalid confirmation link. Link may have expired.");
            }
            return new MessageResponse("User confirmed.");
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(DotEnv.Read()["SECRET"].PadLeft(32));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
