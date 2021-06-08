using BlazorServerAPI.Handlers;
using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using BlazorServerAPI.Services;
using BlazorServerAPI.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BlazorServerAPI.Utils;

namespace BlazorServerAPI.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AuthHandler _handler;

        public AuthController(UserRepository userService, SecurityRepository securityService, IMailService mailService)
        {
            _handler = new AuthHandler(userService, securityService, mailService);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                var responseOnRegister = await _handler.Register(user);
                return StatusCode(StatusCodes.Status201Created, responseOnRegister.ToString());
            }
            catch (Exception e)
            {
                if (e is MongoDB.Driver.MongoWriteException)
                {
                    return BadRequest(new ErrorResponse(error: Text.EmailUsed).ToString());
                }
                if (e is ServerException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: Text.ServerException, errorMessage: e.ToString()).ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: Text.InternalServerError, errorMessage: e.ToString()).ToString());
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                if (HttpContext.Connection.RemoteIpAddress != null)
                {
                    var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                    if (string.IsNullOrEmpty(ip))
                    {
                        ip = Text.Unknown;
                    }
                    var responseOnLogin = await _handler.Login(user, ip);
                    return StatusCode(StatusCodes.Status202Accepted, responseOnLogin.ToString());
                }
            }
            catch (Exception e)
            {
                if (e is ServerException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: Text.ServerException, errorMessage: e.ToString()).ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: Text.InternalServerError, errorMessage: e.ToString()).ToString());
            }
            return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse(error: Text.Forbidden).ToString());
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> Confirm(string userId)
        {
            try
            {
                var responseOnConfirm = await _handler.Confirm(userId);
                return StatusCode(StatusCodes.Status202Accepted, responseOnConfirm.ToString());
            }
            catch (Exception e)
            {
                if (e is FormatException)
                {
                    return BadRequest(new ErrorResponse(error: Text.InvalidConfirmationString(userId)).ToString());
                }
                if (e is ServerException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: Text.ServerException, errorMessage: e.ToString()).ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: Text.InternalServerError, errorMessage: e.ToString()).ToString());
            }
        }

        [HttpPost("reset_request")]
        public async Task<IActionResult> ResetPasswordMailRequest([FromBody] User user)
        {
            try
            {
                var responseOnReset = await _handler.ResetPasswordMailRequest(user);
                return StatusCode(StatusCodes.Status202Accepted, responseOnReset.ToString());
            }
            catch (Exception e)
            {
                if (e is ServerException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: Text.ServerException, errorMessage: e.ToString()).ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: Text.InternalServerError, errorMessage: e.ToString()).ToString());
            }
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] User user)
        {
            try
            {
                var responseOnReset = await _handler.ResetPassword(user);
                return StatusCode(StatusCodes.Status202Accepted, responseOnReset.ToString());
            }
            catch (Exception e)
            {
                if (e is ServerException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: Text.ServerException, errorMessage: e.ToString()).ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: Text.InternalServerError, errorMessage: e.ToString()).ToString());
            }
        }
    }
}
