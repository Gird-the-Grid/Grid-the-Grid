using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Services;

namespace IntegrationTests
{
    public class IntegrationTest1
    {
        static readonly WebApplicationFactory<BlazorServerAPI.Startup> _factory = new WebApplicationFactory<BlazorServerAPI.Startup>();
        static readonly HttpClient _client = _factory.CreateClient();

    
        private readonly User _existentUser = new User();
        private readonly User _newUser = new User("first@mail.com", "testPassword1");
        public IMailService mailService = new MailService();

        //PASS
        [Fact]
        public async Task Get_Should_Retrieve_Forecast()
        {
            var request = await _client.GetAsync("/WeatherForecast");
            var okResult = request.StatusCode;
            Console.Write(okResult);
            Assert.True(StatusCodes.Status200OK.Equals(request.StatusCode));

        }

        //PASS
        [Fact]
        public async Task Login_Success_StatusCode()
        {
            
            var request = await _client.PostAsJsonAsync("/auth/login", _existentUser);
            Assert.False(StatusCodes.Status200OK.Equals(request.StatusCode));
            

        }

        //PASS
        [Fact]
        public async Task Login_False_StatusCode()
        {

            var request = await _client.PostAsJsonAsync("/auth/login", _newUser);
            var response = await request.Content.ReadAsStringAsync();
            Console.Write(response.ToString());
            Assert.False(StatusCodes.Status200OK.Equals(request.StatusCode));
        }

        //PASS
        [Fact]
        public async Task Register_False_StatusCode()
        {

            var request = await _client.PostAsJsonAsync("/auth/register", _existentUser);
            Assert.False(StatusCodes.Status200OK.Equals(request.StatusCode));

        }


    }


}
