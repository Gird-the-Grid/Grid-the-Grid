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
            //TODO: Add delete routes with id for company configuration and grid parameters
            _companyHandler = new ControlPanelCompanyHandler(companyRepository);
            _gridHandler = new ControlPanelGridHandler(gridRepository);
        }

        [HttpPost("configuration")]
        public async Task<IActionResult> CreateCompanyConfiguration([FromBody] CompanyModel companyConfiguration)
        {
            if (!ModelState.IsValid)
            {
                //TODO: use FluentValidation
                return BadRequest(new ErrorResponse(error: ModelState.Values.ToString()).ToString());
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
                    return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse("User doesn't own this resource").ToString());
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
            try
            {
                if (userId != HttpContext.Items["UserId"].ToString())
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse("User doesn't own this resource").ToString());
                }
                var companyConfiguration = await _companyHandler.GetCompanyConfiguration(userId);
                return StatusCode(StatusCodes.Status200OK, companyConfiguration.ToString());
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

        [HttpPost("grid")]
        public async Task<IActionResult> CreateGridParameters([FromBody] GridTemplate gridTemplate)
        {
            if (!ModelState.IsValid)
            {
                //TODO: use FluentValidation
                return BadRequest(new ErrorResponse(error: ModelState.Values.ToString()).ToString());
            }
            try
            {
                gridTemplate.OwnerId = HttpContext.Items["UserId"].ToString();
                var response = await _gridHandler.CreateGridParameters(gridTemplate);
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

        [HttpPut("grid")]
        public async Task<IActionResult> UpdateGridParameters([FromBody] GridTemplate grid)
        {
            if (!ModelState.IsValid)
            {
                //TODO: use FluentValidation
                return BadRequest(new ErrorResponse(error: ModelState.Values.ToString()));
            }
            try
            {
                if (grid.OwnerId != HttpContext.Items["UserId"].ToString())
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse("User doesn't own this resource").ToString());
                }
                var response = await _gridHandler.UpdateGridParameters(grid);
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

        [HttpGet("grid")]
        public async Task<IActionResult> GetGridParameters(string userId)
        {
            try
            {
                if (userId != HttpContext.Items["UserId"].ToString())
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse("User doesn't own this resource").ToString());
                }
                var gridParameters = await _gridHandler.GetGridParameters(userId);
                return StatusCode(StatusCodes.Status200OK, gridParameters.ToString());
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
    }


}
