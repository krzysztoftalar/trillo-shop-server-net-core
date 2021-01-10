using Application.Interfaces;
using Infrastructure.Payment;
using Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, StripePaymentService>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();

            return services;
        }
    }
}