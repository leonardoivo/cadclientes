using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Tiradentes.CobrancaAtiva.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static void SwaggerServiceConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(swg =>
            {
                swg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tiradentes - Cobrança Ativa",
                });
                swg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Autenticação JWT. Exemplo: Bearer <token>"
                });
                swg.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swg.IncludeXmlComments(xmlPath);
            });
        }

        public static void SwaggerApplicationConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Tiradentes - Cobrança Ativa | API v1.0");
            });
        }
    }
}
