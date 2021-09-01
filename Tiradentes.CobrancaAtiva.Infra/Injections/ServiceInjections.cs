using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tiradentes.CobrancaAtiva.Services.Interface.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;

namespace Tiradentes.CobrancaAtiva.Infra.Injections
{
    public class ServiceInjections
    {
        public static void RegisterAppService(IServiceCollection services)
        {
            services.AddScoped<IAlunoService, AlunoService>();
        }
    }
}
