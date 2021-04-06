using BlazorServerAPI.Handlers;
using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Requests;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlazorServerAPI.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

       private readonly AuthHandler _handler;

       public AuthController(UserRepository userService)
        {
            _handler = new AuthHandler(userService);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                //TODO: use FluentValidation
                //TODO: Check if this ModelState refers to RegisterUser and not user
                return BadRequest(new ErrorResponse(error: ModelState.Values.ToString()));
            }
            try
            {
                var x = await _handler.Register(user);
                return StatusCode(StatusCodes.Status201Created, x.ToString());
            }
            catch (MongoDB.Driver.MongoWriteException)
            {
                return BadRequest(new ErrorResponse(error: "Email used").ToString());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var x = await _handler.Login(user);
                return StatusCode(StatusCodes.Status202Accepted, x.ToString());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
        }
    }
}
