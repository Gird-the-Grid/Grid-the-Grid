using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;
using BlazorServer.Models.Responses;

namespace BlazorServer.Handlers
{
    public class AuthHandler
    {
        private readonly UserService _userService;

        public AuthHandler(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IResponse> Register(User user)
        {
            var x = new PasswordHasher<object?>().HashPassword(null, user.Password);
            if (x == null)
            {
                throw new Exception("PasswordHasher failed, not enough entropy");
            }
            user.Password = x;
            await _userService.Create(user);
            return new MessageResponse("User created");
        }

        public async Task<IResponse> Login(User user)
        {
            //TODO: Get this through middleware to reject invalid requests
            var result = await _userService.Find(user.Email);
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
                return new LoginResponse(token: "None");
            }
            throw new Exception(passwordVerificationResult.ToString());
        }
    }
}
