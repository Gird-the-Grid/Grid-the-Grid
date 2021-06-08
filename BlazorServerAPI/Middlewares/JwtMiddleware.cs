using dotenv.net;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorServerAPI.Utils;

namespace BlazorServerAPI.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers[Text.Authorization].FirstOrDefault()?.Split(Text.Space).Last();
            try
            {
                attachUserToContext(context, token);
            } 
            catch
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(Text.InvalidToken);
                return;
            }
            await _next(context);
        }

        private static void attachUserToContext(HttpContext context, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(DotEnv.Read()[Text.Secret].PadLeft(32));
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == Text.Id).Value;
            context.Items[Text.UserId] = userId;
        }

    }
}
