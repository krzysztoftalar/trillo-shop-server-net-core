using Application;
using Application.Interfaces;
using Infrastructure;
using Infrastructure.Payment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Stripe;
using System;
using WebUI.Configurations;
using FluentValidation.AspNetCore;
using WebUI.Middleware;

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
            
            services.AddControllers()
                .AddFluentValidation(options => { options.RegisterValidatorsFromAssemblyContaining<IAppDbContext>(); });;

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
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddJwtIdentity(_config.GetSection("JwtConfiguration"));
            
            services.Configure<StripeOptions>(_config.GetSection("Stripe"));
            StripeConfiguration.ApiKey = _config.GetSection("Stripe")["SecretKey"];
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
            
            app.UseCookiePolicy();
            app.UseSession();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerDocumentation();
            app.UseAppContext();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}