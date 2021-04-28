using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class IndexPageTests : IClassFixture<CustomWebApplicationFactory<BlazorServerAPI.Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<BlazorServerAPI.Startup> _factory;

        public IndexPageTests(
            CustomWebApplicationFactory<BlazorServerAPI.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}
