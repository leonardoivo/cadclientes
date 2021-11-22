namespace Tiradentes.CobrancaAtiva.Application.Configuration
{
    public class RabbitMQConfig
    {
        public string Queue { get; set; }
        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}