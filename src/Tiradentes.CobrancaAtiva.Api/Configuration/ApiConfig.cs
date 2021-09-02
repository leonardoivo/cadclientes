using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiradentes.CobrancaAtiva.Application.Middlewares;

namespace Tiradentes.CobrancaAtiva.Api.Configuration
{
    public static class ApiConfig
    {

        public static void ApiServiceConfig(this IServiceCollection services)
        {
            services.AddControllers();
        }

        public static void ApiApplicationConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExcpetionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
