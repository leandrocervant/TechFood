using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechFood.Domain.Repositories;
using TechFood.Domain.UoW;
using TechFood.Infra.Data.Contexts;
using TechFood.Infra.Data.Repositories;
using TechFood.Infra.Data.UoW;

namespace TechFood.Infra.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraData(this IServiceCollection services)
        {
            //Context
            services.AddScoped<TechFoodContext>();
            services.AddDbContext<TechFoodContext>((serviceProvider, options) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();

                options.UseLazyLoadingProxies();
                options.UseSqlServer(config.GetConnectionString("DataBaseConection"));
            });

            //UoW
            services.AddScoped<IUnitOfWorkTransaction, UnitOfWorkTransaction>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWork, AnotherUnitOfWork>();

            //Data
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
