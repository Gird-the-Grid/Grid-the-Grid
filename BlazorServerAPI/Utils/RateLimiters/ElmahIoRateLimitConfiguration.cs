using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BlazorServerAPI.Utils.RateLimiters
{
    public class ElmahIoRateLimitConfiguration : RateLimitConfiguration
    {
        public ElmahIoRateLimitConfiguration(IHttpContextAccessor httpContextAccessor, IOptions<IpRateLimitOptions> ipOptions, IOptions<ClientRateLimitOptions> clientOptions)
            : base(httpContextAccessor, ipOptions, clientOptions)
        { }

        protected override void RegisterResolvers()
        {
            ClientResolvers.Add(new ClientQueryStringResolveContributor(HttpContextAccessor));
        }
    }
}
