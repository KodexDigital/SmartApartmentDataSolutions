using Application.Common.ApplicationProperties;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Application.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"Title: {Information.APP_NAME}",
                    Version = $"Version: {Information.VERSION}",
                    Description = $"Description: {Information.DESCRIPTION}",
                    Contact = new OpenApiContact
                    {
                        Name = "Contact Name: Kenneth Otoro [KODEX]",
                        Email = "kodexkenth@gmail.com",
                        Url = new Uri("https://github.com/KodexDigital/SmartApartmentDataSolutions")
                    },
                    License = new OpenApiLicense
                    {
                        Name = $"Licensed by: {Information.LICENSE}",
                        Url = new Uri("https://smartapartmentdata.com/")
                    }
                }); ;
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Authentication header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[]{"Bearer"} }
                });
                c.IncludeXmlComments(Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml"));
            });
        }

    }
}
