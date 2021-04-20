using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace BlazorServerAPI.Middlewares
{
    public class ConfigurationCheckerMiddleware
    {
        private readonly RequestDelegate _next;


        public ConfigurationCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, CompanyRepository companyRepository, GridRepository gridRepository)
        {
            var userId = (string) context.Items["UserId"];
            try
            {
                await attackConfigurationToContext<CompanyModel>(context, "Company", companyRepository, userId);
                await attackConfigurationToContext<GridModel>(context, "Grid", gridRepository, userId);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync($"Exception: {e.ToString()}, {e.Message}");
                return;
            }
            await _next(context);
        }


        private async Task attackConfigurationToContext<T>(HttpContext context, string configurationType, OwnedResourceRepository<T> ownedResourceRepository, string userId) where T : OwnedEntity
        {
            var configuration = await ownedResourceRepository.GetObject(userId);
            context.Items[configurationType] = configuration;
        }
    }
}
