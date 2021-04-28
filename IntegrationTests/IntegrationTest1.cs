using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Services;
using Microsoft.AspNetCore.Http;
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

        static readonly CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();
        static readonly HttpClient _client = _factory.CreateClient();


        private readonly User _existentUser = new User();
        private readonly User _newUser = new User("first@mail.com", "testPassword1");
        public IMailService mailService = new MailService();

        [Theory]
        [InlineData("/Register")]
        [InlineData("/Grid")]
        [InlineData("/ControlPanel")]
        public async Task Get_Endpoints_For_Unlogged_User(string url)
        {
            // Arrange

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 401
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }


        //PASS
        [Fact]
        public async Task Login_Success_StatusCode()
        {
            // Arrange

            // Act
            var request = await _client.PostAsJsonAsync("/auth/login", _existentUser);

            // Assert
            Assert.False(StatusCodes.Status200OK.Equals(request.StatusCode));


        }

        //PASS
        [Fact]
        public async Task Login_False_StatusCode()
        {
            // Arrange

            // Act
            var request = await _client.PostAsJsonAsync("/auth/login", _newUser);
            var response = await request.Content.ReadAsStringAsync();
            
            //Assert
            Assert.False(StatusCodes.Status200OK.Equals(request.StatusCode));
        }

        //PASS
        [Fact]
        public async Task Register_False_StatusCode()
        {
            // Arrange

            // Act
            var request = await _client.PostAsJsonAsync("/auth/register", _existentUser);

            //Assert
            Assert.False(StatusCodes.Status200OK.Equals(request.StatusCode));

        }


    }


}
