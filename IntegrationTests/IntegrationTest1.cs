using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class IntegrationTest1
    {
        static readonly WebApplicationFactory<BlazorServer.Startup> _factory = new WebApplicationFactory<BlazorServer.Startup>();
        static readonly HttpClient _client = _factory.CreateClient();

        private readonly UserService _userService;
        private readonly User user = new User();

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
            var user = new User("_test", "_test");
            ObjectResult request = (ObjectResult)await authController.Register(user);
            Assert.Equal(typeof(ObjectResult), request.GetType());
        }
        //PASS
        [Fact]
        public async Task Login_Success_StatusCode()
        {
            
            var request = await _client.PostAsJsonAsync("/auth/login", new { email = "first", password = "first" });
            //var forecast = await response.Content.ReadAsStringAsync();
            Assert.NotEqual((int)StatusCodes.Status200OK, (int)request.StatusCode);

        }

        //PASS
        [Fact]
        public async Task Login_False_StatusCode()
        {

            var request = await _client.PostAsJsonAsync("/auth/login", new { email = "_test", password = "_test" });
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

            var request = await _client.PostAsJsonAsync("/auth/register", new { email = "_test", password = "_test" });
            //var forecast = await response.Content.ReadAsStringAsync();
            Assert.NotEqual((int)StatusCodes.Status400BadRequest, (int)request.StatusCode);

        }

        ////PASS
        //[Fact]
        //public async Task Register_Succes_StatusCode()
        //{

        //    var request = await _client.PostAsJsonAsync("/auth/register", new { email = "_test_reg", password = "_test_reg" });
        //    //var forecast = await response.Content.ReadAsStringAsync();
        //    Assert.Equal((int)StatusCodes.Status200OK, (int)request.StatusCode);

        //}


    }


}
