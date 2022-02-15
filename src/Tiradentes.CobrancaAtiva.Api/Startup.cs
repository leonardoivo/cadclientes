using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiradentes.CobrancaAtiva.Api.Configuration;
using Tiradentes.CobrancaAtiva.Api.Workers;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using Tiradentes.CobrancaAtiva.CrossCutting.IoC;
using Tiradentes.CobrancaAtiva.Services.Consumers;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.Configure<RabbitMQConfig>(_configuration.GetSection("RabbitMQ"));
            services.Configure<EncryptationConfig>(_configuration.GetSection("Encryptation"));
            services.AddDependencies(_configuration);
            services.ApiServiceConfig();
            services.AutoMapperServiceConfig();
            services.SwaggerServiceConfig();
            services.AddAuthenticationConfig(_configuration);

            // services.AddHostedService<GerenciarCobrancaRetornoWorker>();
            // services.AddHostedService<RegraNegociacaoWorker>();
            // services.AddHostedService<ParametroEnvioConsumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.SwaggerApplicationConfig();
            }
            app.UseMetrics();
            app.UseAuthentication();
            app.UseAuthorization();
            app.ApiApplicationConfig(env);
        }
    }
}