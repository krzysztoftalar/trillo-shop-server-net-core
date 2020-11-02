using System;
using Application;
using Application.Interfaces;
using Infrastructure;
using Infrastructure.Payment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Persistence;
using WebUI.Configurations;
using WebUI.Infrastructure;
using WebUI.Middleware;
using AppContext = Application.Interfaces.AppContext;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        private readonly IConfiguration _config;
        private const string CorsPolicy = "CorsPolicy";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistence(_config);
            services.AddApplication();
            services.AddInfrastructure();

            services.AddSwaggerDocumentation();
            services.AddHttpContextAccessor();

            services.AddCors(options =>
                options.AddPolicy(CorsPolicy,
                    policy =>
                    {
                        policy
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins(_config.GetSection("Routing")["Client"])
                            .AllowCredentials()
                            .WithExposedHeaders("WWW-Authenticate");
                    }
                ));

            services.AddSession(options =>
            {
                options.Cookie.Name = "Cart";
                options.Cookie.MaxAge = TimeSpan.FromMinutes(60);
            });

            services.AddScoped<ISessionService, SessionService>();

            services.Configure<StripeOptions>(_config.GetSection("Stripe"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();
            app.UseCors(CorsPolicy);

            app.UseSession();

            app.UseSwaggerDocumentation();
            app.UseAppContext();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}