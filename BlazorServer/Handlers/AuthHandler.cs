using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using BlazorServerAPI.Repository;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Requests;

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
                return new LoginResponse(token: "None");
            }
            throw new Exception(passwordVerificationResult.ToString());
        }
    }
}
