using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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
