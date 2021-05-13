using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using System;

namespace BlazorServerAPI.Utils.RateLimiters
{
    public class ClientQueryStringResolveContributor : IClientResolveContributor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ClientQueryStringResolveContributor(IHttpContextAccessor httpContextAccessor) => this.httpContextAccessor = httpContextAccessor;

        public string ResolveClient()
        {
            var request = httpContextAccessor.HttpContext?.Request;
            if (request == null)
            {
                return "";
            }
            var queryDictionary =Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(request.QueryString.ToString());
            if (queryDictionary.ContainsKey("api_key") && !string.IsNullOrWhiteSpace(queryDictionary["api_key"]))
            {
                return queryDictionary["api_key"];
            }
            return Guid.NewGuid().ToString();
        }
    }
}
