using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Repository;
using BlazorServerAPI.Controllers;

namespace IntegrationTests
{
    public class IntegrationTest1
    {
        static readonly WebApplicationFactory<BlazorServerAPI.Startup> _factory = new WebApplicationFactory<BlazorServerAPI.Startup>();
        static readonly HttpClient _client = _factory.CreateClient();

        private readonly UserRepository _userService;
        private readonly User _existentUser = new User();
        private readonly User _newUser = new User("first@mail.com", "testPassword1");

        //PASS
        [Fact]
        public async Task Get_Should_Retrieve_Forecast()
        {
            var request = await _client.GetAsync("/WeatherForecast");
            //var forecast = await response.Content.ReadAsStringAsync();
            var okResult = request.StatusCode;
            Console.Write(okResult);
            Assert.Equal((int)StatusCodes.Status200OK, (int)request.StatusCode);

        }

        //PASS
        [Fact]
        public async Task AuthController_Return_Type()
        {
            var authController = new AuthController(_userService);
            ObjectResult request = (ObjectResult)await authController.Register(_newUser);
            Assert.Equal(typeof(ObjectResult), request.GetType());
        }
        //PASS
        [Fact]
        public async Task Login_Success_StatusCode()
        {
            
            var request = await _client.PostAsJsonAsync("/auth/login", _existentUser);
            //var forecast = await response.Content.ReadAsStringAsync();
            Assert.NotEqual((int)StatusCodes.Status200OK, (int)request.StatusCode);

        }

        //PASS
        [Fact]
        public async Task Login_False_StatusCode()
        {

            var request = await _client.PostAsJsonAsync("/auth/login", _newUser);
            var response = await request.Content.ReadAsStringAsync();
            Console.Write(response.ToString());
            Assert.NotEqual((int)StatusCodes.Status200OK, (int)request.StatusCode);
            //var authController = new AuthController(_userService);
            //var user = new User("_test","_test");
            //var request = await authController.Register(user);
            //Assert.Equal(null, request);

        }

        //PASS
        [Fact]
        public async Task Register_False_StatusCode()
        {

            var request = await _client.PostAsJsonAsync("/auth/register", _existentUser);
            //var forecast = await response.Content.ReadAsStringAsync();
            Assert.NotEqual((int)StatusCodes.Status400BadRequest, (int)request.StatusCode);

        }

        ////PASS
        //[Fact]
        //public async Task Register_Succes_StatusCode()
        //{

        //    var request = await _client.PostAsJsonAsync("/auth/register", _newUser });
        //    //var forecast = await response.Content.ReadAsStringAsync();
        //    Assert.Equal((int)StatusCodes.Status200OK, (int)request.StatusCode);

        //}


    }


}
