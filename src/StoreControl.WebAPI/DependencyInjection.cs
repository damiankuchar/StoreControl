using Microsoft.OpenApi.Models;
using StoreControl.WebAPI.Middlewares;
using StoreControl.WebAPI.OptionsSetup;
using System.Reflection;

namespace StoreControl.WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.ConfigureOptionsInjection();
            services.ConfigureExceptionHandling();
            services.ConfigureCorsPolicy();
            services.ConfigureSwaggerInjection();

            return services;
        }

        private static IServiceCollection ConfigureOptionsInjection(this IServiceCollection services)
        {
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            return services;
        }

        private static IServiceCollection ConfigureExceptionHandling(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }

        private static IServiceCollection ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed(_ => true);
                });
            });

            return services;
        }

        private static IServiceCollection ConfigureSwaggerInjection(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "StoreControl",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "damiankuchar1@gmail.com",
                        Url = new Uri("https://github.com/damiankuchar")
                    }
                });

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                opt.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
