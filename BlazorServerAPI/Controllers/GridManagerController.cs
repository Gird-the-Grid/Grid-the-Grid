﻿using BlazorServerAPI.Handlers;
using BlazorServerAPI.Models.Responses;
using BlazorServerAPI.Repository;
using BlazorServerAPI.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BlazorServerAPI.Utils;


namespace BlazorServerAPI.Controllers
{
    //TODO: check the class and methods and rename if needed
    [Route("grid_manager/")]
    [ApiController]
    public class GridManagerController : ControllerBase
    {
        private readonly GridManagerHandler _gridManagerHandler;

        public GridManagerController(GridRepository gridRepository)
        {
            _gridManagerHandler = new GridManagerHandler(gridRepository);
        }

        [HttpGet("current_weights")]
        public async Task<IActionResult> GetCurrentWeights(string userId)
        {
            try
            {
                if (userId != HttpContext.Items[Text.UserId]?.ToString())
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse(Text.UnownedResource).ToString());
                }
                var currentWeights = await _gridManagerHandler.GetCurrentWeights(userId);
                return StatusCode(StatusCodes.Status200OK, currentWeights.ToString());
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

        [HttpGet("energy_cost")]
        public async Task<IActionResult> GetEnergyCost()
        {
            try
            {
                var energyCost = await _gridManagerHandler.GetEnergyCost();
                return StatusCode(StatusCodes.Status200OK, energyCost.ToString());
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
