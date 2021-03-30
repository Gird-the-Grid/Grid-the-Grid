using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;
using Microsoft.AspNetCore.Identity;

namespace BlazorServer.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            //TODO: make this await and async
            try
            {
                //TODO: Get this through middleware to reject invalid requests
                user.Password = new PasswordHasher<object?>().HashPassword(null, user.Password);
                if (user.Password == null)
                {
                    throw new Exception("PasswordHasher failed, not enough entropy");
                }
                var result = _userService.Create(user);
                return Ok(new { success = true, message = "User created", user = result, todo = "remove $user in release" });
            }
            catch (MongoDB.Driver.MongoWriteException e )
            {
                return BadRequest(new { success = false, error = "Email used.", errorMessage = e.ToString(), todo = "remove $errorMessage in release" } );
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, error = "Something went wrong", errorMessage = e.ToString(), todo = "remove $errorMessage in release" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            //TODO: make this await and async
            try
            {
                //TODO: Get this through middleware to reject invalid requests
                var result = _userService.Find(user.Email);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, (new { success = false, error = "invalid credentials"}));
                }
                var passwordVerificationResult = new PasswordHasher<object?>().VerifyHashedPassword(null, result.Password, user.Password);
                if (passwordVerificationResult == PasswordVerificationResult.Failed)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, (new { success = false, error = "invalid credentials"}));
                }
                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    return Ok(new { success = true, token = "None", todo = "add jwt token" });
                }
                throw new Exception("?\n" + passwordVerificationResult.ToString());
            }
            catch (MongoDB.Driver.MongoWriteException e)
            {
                return BadRequest(new { success = false, errorMessage = e.ToString(), todo = "remove $errorMessage in release" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, error = "Something went wrong", errorMessage = e.ToString(), todo = "remove $errorMessage in release" });
            }
        }
    }
}
