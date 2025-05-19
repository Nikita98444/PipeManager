
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PipeService.Repositories;
using System.Reflection;
using FluentValidation;

namespace PipeService
{
    public static class DependencyInjection
    {
        public static IServiceCollection PipeServices(
                        this IServiceCollection services,
                        IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IPipesRepository, PipesRepository>();
            return services;
        }
    }
}
