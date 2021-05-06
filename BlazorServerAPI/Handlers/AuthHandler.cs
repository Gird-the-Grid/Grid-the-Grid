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
        private readonly SecurityRepository _securityService;
        private readonly IMailService _mailService;

        public AuthHandler(UserRepository userService, SecurityRepository securityService, IMailService mailService)
        {
            _userService = userService;
            _securityService = securityService;
            _mailService = mailService;
        }

        public async Task<IResponse> Register(User user)
        {
#nullable enable
            var hashedPassword = new PasswordHasher<object?>().HashPassword(null, user.Password);
#nullable disable
            if (hashedPassword == null)
            {
                throw new InvalidPasswordException("PasswordHasher failed, not enough entropy");
            }
            var newUser = new User(user.Email, hashedPassword);
            newUser = await _userService.CreateUser(newUser);
            await _mailService.SendEmailAsync(new ConfirmRegistrationMailRequest(newUser.Email, newUser.Id));
            return new MessageResponse("User created. Confirmation Mail Sent.");
        }

        public async Task<IResponse> Login(User user, string ip)
        {
            var result = await _userService.FindUserByEmail(user.Email);
            if (result == null)
            {
                return new ErrorResponse(error: "Invalid credentials");
            }
#nullable enable
            var passwordVerificationResult = new PasswordHasher<object?>().VerifyHashedPassword(null, result.Password, user.Password);
#nullable disable
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                var penetrationReport = await _securityService.RegisterNewAttack(user.Email, ip);
                if (penetrationReport.PasswordAtempts > 10)
                {
                    await _mailService.SendEmailAsync(new PenetrationReportMailRequest(user.Email, penetrationReport));
                }
                return new ErrorResponse(error: "Invalid credentials");
            }
            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                return new LoginResponse(token: generateJwtToken(result), userId: result.Id);
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

        public async Task<IResponse> ResetPasswordMailRequest(User user)
        {
            var result = await _userService.FindUserByEmail(user.Email);
            if (result == null)
            {
                return new ErrorResponse(error: "Invalid email");
            }
            await _mailService.SendEmailAsync(new ResetPasswordMailRequest(result.Email, result.Id));
            return new MessageResponse("Reset Password Mail Sent.");
        }
        public async Task<IResponse> ResetPassword(User user)
        {
#nullable enable
            var hashedPassword = new PasswordHasher<object?>().HashPassword(null, user.Password);
#nullable disable
            if (hashedPassword == null)
            {
                throw new InvalidPasswordException("PasswordHasher failed, not enough entropy");
            }
            var result = await _userService.ResetPassword(user.Id, hashedPassword);
            if (result == null)
            {
                return new ErrorResponse(error: "Invalid user id");
            }
            return new MessageResponse("Password has been reset.");
        }

        private static string generateJwtToken(User user)
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
