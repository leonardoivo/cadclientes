using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Workers
{
    public class RegraNegociacaoWorker : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public RegraNegociacaoWorker(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                int hourSpan = DateTime.Now.Hour;
                int numberOfHours = hourSpan;

                if (hourSpan == 9)
                {
                    Process();
                    numberOfHours = 9;
                }

                await Task.Delay(TimeSpan.FromHours(numberOfHours), stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private void Process()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IRegraNegociacaoService>();
                _service.InativarRegrasNegociacao();
                _service.AtivarRegrasNegociacao();
            }
        }
    }
}