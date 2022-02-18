using System.Reflection.Emit;
using Microsoft.AspNetCore.Builder;
using Prometheus;

namespace Tiradentes.CobrancaAtiva.Api.Configuration
{
    public static class MetricsConfig
    {
        public static void UseMetrics(this IApplicationBuilder app)
        {
            var counter = Metrics.CreateCounter("mecapimetric", "Conta requests nos endpoints", new CounterConfiguration
            {
                LabelNames = new[] {"method", "endpoint"}
            });
            app.Use((context, next) =>
            {
                counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
                return next();
            });

            app.UseMetricServer();
            app.UseHttpMetrics();
        }
    }
}