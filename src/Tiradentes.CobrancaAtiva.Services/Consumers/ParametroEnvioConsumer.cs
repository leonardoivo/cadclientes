using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Consumers
{
    public class ParametroEnvioConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMQConfig _rabbitMQConfig;

        public ParametroEnvioConsumer(IServiceProvider serviceProvider, IOptions<RabbitMQConfig> rabbitMQConfig)
        {
            _rabbitMQConfig = rabbitMQConfig.Value;
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQConfig.HostName,
                VirtualHost = _rabbitMQConfig.VirtualHost,
                UserName = _rabbitMQConfig.UserName,
                Password = _rabbitMQConfig.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                        queue: _rabbitMQConfig.QueueEnvioArquivo,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);

                var mensagem = JsonSerializer.Deserialize<EnvioLoteViewModel>(contentString);

                _ = EnviarArquivoLote(mensagem);
                
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };
            _channel.BasicConsume(_rabbitMQConfig.QueueEnvioArquivo, false, consumer);

            return Task.CompletedTask;
        }

        private async Task EnviarArquivoLote(EnvioLoteViewModel envioLote)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var parametroEnvioService = scope.ServiceProvider.GetRequiredService<IParametroEnvioService>();
                
                await parametroEnvioService.EnviarArquivoEmpresaCobranca(envioLote.IdParametroEnvio, envioLote.Lote);
            }
        }
    }
}
