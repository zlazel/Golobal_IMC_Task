using AutoMapper;
using Golobal_IMC_Task.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Golobal_IMC_Task.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
