using BlazorServerAPI.Models.Entities;
using BlazorServerAPI.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using BlazorServerAPI.Utils;

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
            var userId = (string) context.Items[Text.UserId];
            try
            {
                await attachConfigurationToContext<CompanyModel>(context, Text.Company, companyRepository, userId);
                await attachConfigurationToContext<GridModel>(context, Text.Grid, gridRepository, userId);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(Text.Exception(e));
                return;
            }
            await _next(context);
        }


        private static async Task attachConfigurationToContext<T>(HttpContext context, string configurationType, OwnedResourceRepository<T> ownedResourceRepository, string userId) where T : OwnedEntity
        {
            var configuration = await ownedResourceRepository.GetObject(userId);
            context.Items[configurationType] = configuration;
        }
    }
}
