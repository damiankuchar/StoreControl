using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StoreControl.Application.Behaviours;
using StoreControl.Application.Shared.Services.PermissionService;
using StoreControl.Application.Shared.Services.RoleService;
using StoreControl.Application.Shared.Services.UserService;
using System.Reflection;

namespace StoreControl.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();

            return services;
        }
    }
}
