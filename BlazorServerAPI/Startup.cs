using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using dotenv.net;
using BlazorServerAPI.Repository;
using BlazorServerAPI.Middlewares;
using BlazorServerAPI.Settings;
using BlazorServerAPI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using BlazorServerAPI.Models.Responses;
using System.Linq;
using AspNetCoreRateLimit;
using System.Collections.Generic;
using BlazorServerAPI.Utils.RateLimiters;

namespace BlazorServerAPI
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });


            #region Mail Settings
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, Services.MailService>();
            #endregion

            #region DB settings
            services.Configure<MongoDbSettings>(
                Configuration.GetSection(nameof(MongoDbSettings))
            );
            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value
            );
            services.AddSingleton<UserRepository>();
            services.AddSingleton<GridRepository>();
            services.AddSingleton<CompanyRepository>();
            services.AddSingleton<SecurityRepository>();
            #endregion

            #region Rate Limiting
            services.AddMemoryCache();
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.Configure<ClientRateLimitOptions>(options => {
                options.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Period = "1m",
                        Limit = 300,
                    },
                };
            });
            services.AddSingleton<IRateLimitConfiguration, ElmahIoRateLimitConfiguration>();
            #endregion

            services.AddControllers()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                    s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = string.Join(", ", context.ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).Select(v => v.ErrorMessage));
                    return new BadRequestObjectResult(new ErrorResponse(error: errors))
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlazorServer", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DotEnv.Load();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlazorServer v1"));
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseWhen(
                context => !context.Request.Path.StartsWithSegments("/auth"),
                appBuilder =>
                {
                    appBuilder.UseMiddleware<JwtMiddleware>();
                    appBuilder.UseMiddleware<ConfigurationCheckerMiddleware>();
                }
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
