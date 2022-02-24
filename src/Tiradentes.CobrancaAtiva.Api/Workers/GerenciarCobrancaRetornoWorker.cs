using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Workers
{
    public class GerenciarCobrancaRetornoWorker : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public GerenciarCobrancaRetornoWorker(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {            

            do
            {
                Process();

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task Process()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IGerenciarArquivoCobrancaRetornoService>();

                try
                {                    
                   await _service.Gerenciar();
                }
                catch (Exception ex)
                {
                    GravaLog(JsonSerializer.Serialize($"{ex.Message} => {ex}"));
                }
            }
        }

        private void GravaLog(string texto)
        {
            var log = $"{DateTime.Now.ToString("G")} - {texto}";

            File.AppendAllText(string.Concat(Environment.CurrentDirectory, "\\LogPdv.txt"), string.Concat(Environment.NewLine, log));
        }
    }
}
