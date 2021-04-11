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
            catch (ServerException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
            //TODO: Add general exception

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
        
        [HttpGet("confirm")]
        public async Task<IActionResult> Confirm(string userId)
        {
            try
            {
                var response = await _handler.Confirm(userId);
                return StatusCode(StatusCodes.Status202Accepted, response.ToString());
            }
            catch (ServerException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
            //TODO: Add general exception if needed. Here there-s a System.FormatException: '6071bd6e3f4909a7120824e21' is not a valid 24 digit hex string.
        }
    }
}
