using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using TechFood.Application.Common.Behaviours;
using TechFood.Application.Common.Services;
using TechFood.Application.Common.Services.Interfaces;

namespace TechFood.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Services
            services.AddTransient<IEmailSender, EmailSender>();

            //AutoMapper
            services.AddAutoMapper(typeof(DependecyInjection));

            //FluentValidation
            services.AddFluentValidation(o =>
            {
                o.AutomaticValidationEnabled = false;
                o.RegisterValidatorsFromAssembly(typeof(DependecyInjection).Assembly);
            });

            //MediatR
            services.AddMediatR(typeof(DependecyInjection));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var mediatR = services.First(s => s.ServiceType == typeof(IMediator));

            services.Replace(ServiceDescriptor.Transient<IMediator, Common.EventualConsistency.Mediator>());
            services.Add(
                new ServiceDescriptor(
                    mediatR.ServiceType,
                    Common.EventualConsistency.Mediator.ServiceKey,
                    mediatR.ImplementationType!,
                    mediatR.Lifetime));

            return services;
        }
    }
}
