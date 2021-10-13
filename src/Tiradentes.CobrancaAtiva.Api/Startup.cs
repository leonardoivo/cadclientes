using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tiradentes.CobrancaAtiva.Api.Configuration;
using Tiradentes.CobrancaAtiva.Api.Workers;
using Tiradentes.CobrancaAtiva.CrossCutting.IoC;

namespace Tiradentes.CobrancaAtiva.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDependencies(_configuration);
            services.ApiServiceConfig();
            services.AutoMapperServiceConfig();
            services.SwaggerServiceConfig();
            
            services.AddHostedService<RegraNegociacaoWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.SwaggerApplicationConfig();
            app.ApiApplicationConfig(env);
        }
    }
}
