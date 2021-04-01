using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;
using BlazorServer.Models.Responses;

namespace BlazorServer.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

       private readonly Handlers.AuthHandler _handler;

       public AuthController(UserService userService)
        {
            _handler = new Handlers.AuthHandler(userService);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                //TODO: Check request trough middleware or check it in _handler
                var x = await _handler.Register(user);
                return Ok(x.ToString());
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
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                //TODO: Check request trough middleware or check it in _handler
                var x = await _handler.Login(user);
                return Ok(x.ToString());
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
    }
}
