using EmailAuth.Application.Interfaces;
using EmailAuth.Application.Services;
using EmailAuth.Infrastructure.Services.Interfaces;
using EmailAuth.Infrastructure.Services;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using EmailAuth.Common.Models;
using EmailAuth.Infrastructure.Clients.Interfaces;
using EmailAuth.Infrastructure.Clients;
using EmailAuth.Common.Mappings;
using EmailAuth.Common.Constants;

namespace EmailAuth
{
    /// <summary>
    /// Extension method to configure services for EmailAuth in ASP.NET Core applications.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <param name="assembly">Optional: The assembly to scan for AutoMapper profiles.</param>
    /// <returns>The modified IServiceCollection instance.</returns>
    public static class EmailAuth
    {
        public static IServiceCollection AddEmailAuth(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
        {
            List<Assembly> assemblies = new();
            if (assembly != null)
            {
                assemblies.Add(assembly);
            }

            assemblies.Add(Assembly.GetExecutingAssembly());

            services.AddHttpContextAccessor();

            // Configure AutoMapper of specified assemblies for profiles
            services.AddAutoMapper(cfg =>
            {
                foreach (var assembly in assemblies)
                {
                    cfg.AddProfile(new MappingProfile(assembly));
                }
            });

            // Register services with dependency injection
            SendGridDetail sendGrid = new()
            {
                ApiKey = configuration[Constants.Email_SendGridApiKey],
                SenderEmail = configuration[Constants.Email_EmailFromAddress],
                SenderDisplayName = configuration[Constants.Email_EmailFromName]
            };

            services.AddScoped<ISendGridClientFactory>(provider => new SendGridClientFactory(sendGrid.ApiKey));
            services.AddScoped<IMailerService>(provider => new SendGridEmailService(sendGrid, provider.GetRequiredService<ISendGridClientFactory>()));

            services.AddSingleton<IContextCurrentUserService, ContextCurrentUserService>();
            services.AddSingleton<IJWTokenService, JWTokenService>();
            services.AddScoped<IEmailAuthUserService, EmailAuthUserService>();

            // Configure Swagger/OpenAPI documentation
            services.AddSwaggerGen(swagger =>
            {
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Name = Constants.Authorization,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = Constants.BearerFormat,
                    In = ParameterLocation.Header,
                    Description = Constants.SwaggerAuthDescription
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] {}
                    }
                });
            }).AddSwaggerGenNewtonsoftSupport();

            // Configure JWT authentication services
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidAudience = configuration[Constants.JWT_ValidAudience],
                        ValidIssuer = configuration[Constants.JWT_ValidIssuer],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[Constants.JWT_Secret])),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add(Constants.IsTokenExpired, Constants.True);
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
