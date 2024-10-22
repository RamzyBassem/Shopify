using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shopify.Application.Abstractions.Behaviors;
using Shopify.Domain.Orders;

namespace Shopify.Application
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(configurations =>
            {

                configurations.RegisterServicesFromAssembly(typeof(ServicesExtension).Assembly);
                configurations.AddOpenBehavior(typeof(LoggingBehavior<,>));

                configurations.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(typeof(ServicesExtension).Assembly);
            services.AddTransient<PricingService>();
            return services;
        }
    }
}
