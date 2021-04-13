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

namespace BlazorServerAPI.Controllers
{
    //TODO: check the class and methods and rename if needed
    [Route("control_panel/")]
    [ApiController]
    public class ControlPanelController : ControllerBase
    {
        private readonly ControlPanelHandler _handler;

        public ControlPanelController()
        {
            //TODO: Add CompanyRepository and GridRepository
            _handler = new ControlPanelHandler();
        }

        [HttpPost("configuration")]
        public async Task<IActionResult> CreateCompanyConfiguration([FromBody] CompanyModel companyConfiguration)
        {
            if (!ModelState.IsValid)
            {
                //TODO: use FluentValidation
                return BadRequest(new ErrorResponse(error: ModelState.Values.ToString()));
            }
            try
            {
                var response = await _handler.CreateCompanyConfiguration(companyConfiguration);
                return StatusCode(StatusCodes.Status201Created, response.ToString());
            }
            catch (Exception e)
            {
                if (e is MongoDB.Driver.MongoWriteException)
                {
                    return BadRequest(new ErrorResponse(error: "This company's configuration already exists").ToString());
                }
                if (e is ServerException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Server exception", errorMessage: e.ToString()).ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
        }

        [HttpPut("configuration")]
        public async Task<IActionResult> ChangeCompanyConfiguration([FromBody] CompanyModel companyConfiguration)
        {
            if (!ModelState.IsValid)
            {
                //TODO: use FluentValidation
                return BadRequest(new ErrorResponse(error: ModelState.Values.ToString()));
            }
            try
            {
                var response = await _handler.CreateCompanyConfiguration(companyConfiguration);
                return StatusCode(StatusCodes.Status201Created, response.ToString());
            }
            catch (Exception e)
            {
                if (e is ServerException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Server exception", errorMessage: e.ToString()).ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
        }

        [HttpGet("configuration")]
        public async Task<IActionResult> GetCompanyConfiguration(string userId)
        {
            try
            {
                var companyConfiguration = await _handler.GetCompanyConfiguration(userId);
                return StatusCode(StatusCodes.Status200OK, companyConfiguration.ToString());
            }
            catch (Exception e)
            {
                //TODO: add exception for when userId doesn't exist
                if (e is ServerException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Server exception", errorMessage: e.ToString()).ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
        }
    }


}
