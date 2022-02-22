using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Workers
{
    public class ParametrosEnvioWorker : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;
        
        public ParametrosEnvioWorker(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {            

            do
            {
                if (DateTime.Now.Hour == 6 && DateTime.Now.Minute == 00)
                {
                    Process();
                    await Task.Delay(TimeSpan.FromHours(23), stoppingToken);
                }
                
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private void Process()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IParametroEnvioService>();
                _service.EnviarParametrosAgendadosConsumer();
            }
        }
    }
}
