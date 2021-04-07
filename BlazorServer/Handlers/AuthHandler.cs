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

namespace BlazorServerAPI.Handlers
{
    public class AuthHandler
    {
        private readonly UserRepository _userService;

        public AuthHandler(UserRepository userService)
        {
            _userService = userService;
        }

        public async Task<IResponse> Register(User user)
        {
            var hashedPassword = new PasswordHasher<object?>().HashPassword(null, user.Password);
            if (hashedPassword == null)
            {
                throw new Exception("PasswordHasher failed, not enough entropy");
            }
            var newUser = new User(user.Email, hashedPassword);
            await _userService.CreateUser(newUser);
            return new MessageResponse("User created");
        }

        public async Task<IResponse> Login(User user)
        {
            //TODO: Get this through middleware to reject invalid requests
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
            throw new Exception(passwordVerificationResult.ToString());
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
