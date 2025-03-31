using Microsoft.Extensions.DependencyInjection;
using TechFood.Application;
using TechFood.Infra.Data;

namespace TechFood.Infra.CrossCutting.IoC
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddIoCServices(this IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfraData();

            return services;
        }
    }
}
