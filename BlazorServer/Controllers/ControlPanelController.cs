using BlazorServerAPI.Handlers;
using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
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
        private readonly ControlPanelCompanyHandler _companyHandler;
        private readonly ControlPanelGridHandler _gridHandler;

        public ControlPanelController(CompanyRepository companyRepository, GridRepository gridRepository)
        {
            //TODO: Add delete routes with id for copany configuration and grid parameters
            _companyHandler = new ControlPanelCompanyHandler(companyRepository);
            _gridHandler = new ControlPanelGridHandler(gridRepository);
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
                companyConfiguration.OwnerId = HttpContext.Items["UserId"].ToString();
                var response = await _companyHandler.CreateCompanyConfiguration(companyConfiguration);
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
        public async Task<IActionResult> UpdateCompanyConfiguration([FromBody] CompanyModel companyConfiguration)
        {
            if (!ModelState.IsValid)
            {
                //TODO: use FluentValidation
                return BadRequest(new ErrorResponse(error: ModelState.Values.ToString()));
            }
            try
            {
                if (companyConfiguration.OwnerId != HttpContext.Items["UserId"].ToString())
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse("User doesn't own this resource"));
                }
                var response = await _companyHandler.UpdateCompanyConfiguration(companyConfiguration);
                return StatusCode(StatusCodes.Status200OK, response.ToString());
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
            //TODO: here we should check that userId is the same as jwt, otherwise refuse
            try
            {
                if (userId != HttpContext.Items["UserId"].ToString())
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse("User doesn't own this resource"));
                }
                var companyConfiguration = await _companyHandler.GetCompanyConfiguration(userId);
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

        [HttpPost("grid")]
        public async Task<IActionResult> CreateGridParameters([FromBody] GridModel gridParameters)
        {
            
            if (!ModelState.IsValid)
            {
                //TODO: use FluentValidation
                return BadRequest(new ErrorResponse(error: ModelState.Values.ToString()));
            }
            try
            {
                var response = await _gridHandler.CreateGridParameters(gridParameters);
                return StatusCode(StatusCodes.Status201Created, response.ToString());
            }
            catch (Exception e)
            {
                //TODO: question: can we create more grids for one user? if so then we should allow more posts and make put requests with gridId and not userId
                if (e is ServerException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Server exception", errorMessage: e.ToString()).ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
        }

        [HttpPut("grid")]
        public async Task<IActionResult> UpdateGridParameters([FromBody] GridModel gridParameters)
        {
            //TODO: here we should check that gridParameters.id is owned by userId from jwt, otherwise refuse
            if (!ModelState.IsValid)
            {
                //TODO: use FluentValidation
                return BadRequest(new ErrorResponse(error: ModelState.Values.ToString()));
            }
            try
            {
                var response = await _gridHandler.UpdateGridParameters(gridParameters);
                return StatusCode(StatusCodes.Status200OK, response.ToString());
            }
            catch (Exception e)
            {
                //TODO: question: can we create more grids for one user? if so then we should allow more posts and make put requests with gridId and not userId
                if (e is ServerException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Server exception", errorMessage: e.ToString()).ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
        }

        [HttpGet("grid")]
        public async Task<IActionResult> GetGridParameters(string gridId)
        {
            //TODO: here we should check that gridId is owned by userId from jwt, otherwise refuse
            try
            {
                var gridParameters = await _gridHandler.GetGridParameters(gridId);
                return StatusCode(StatusCodes.Status200OK, gridParameters.ToString());
            }
            catch (Exception e)
            {
                //TODO: add exception for when gridId doesn't exist
                if (e is ServerException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Server exception", errorMessage: e.ToString()).ToString());
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(error: "Internal server error", errorMessage: e.ToString()).ToString());
            }
        }
    }


}
