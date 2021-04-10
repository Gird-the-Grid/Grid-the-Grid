using BlazorServerAPI.Handlers;
using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using BlazorServerAPI.Services;
using BlazorServerAPI.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorServerAPI.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

       private readonly AuthHandler _handler;

       public AuthController(UserRepository userService, IMailService mailService)
        {
            _handler = new AuthHandler(userService, mailService);
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
            //TODO: maybe, maybe make a custom exception class for this exception
            catch (MongoDB.Driver.MongoWriteException)
            {
                return BadRequest(new ErrorResponse(error: "Email used").ToString());
            }
            catch (ServerException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse(error: ModelState.Values.ToString()));
            }
            try
            {
                var x = await _handler.Login(user);
                return StatusCode(StatusCodes.Status202Accepted, x.ToString());
            }
            catch (ServerException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
        }
    }
}
