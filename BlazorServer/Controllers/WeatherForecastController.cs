using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var dbClient = new MongoClient("mongodb+srv://standard_user:standard_password$$$$$$$@cluster0.vdvzo.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            var dbList = dbClient.ListDatabases().ToList();

            var randomGrades = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = randomGrades.Next(-20, 55),
                Summary = dbList[randomGrades.Next(dbList.Count)].ToString()
            })
            .ToArray();
        }
    }
}
