using Application.Interfaces;
using Infrastructure.Payment;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, StripePaymentService>();

            return services;
        }
    }
}