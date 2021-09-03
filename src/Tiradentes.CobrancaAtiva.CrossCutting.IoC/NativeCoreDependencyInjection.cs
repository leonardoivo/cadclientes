using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;

namespace Tiradentes.CobrancaAtiva.CrossCutting.IoC
{
    public static class NativeCoreDependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            #region Repositorios
            services.AddScoped<IEmpresaParceiraRepository, EmpresaParceiraRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            #endregion

            #region Services
            services.AddScoped<IEmpresaParceiraService, EmpresaParceiraService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            #endregion

            //services.AddDbContext<CobrancaAtivaDbContext>(options =>
            //    options.UseInMemoryDatabase("CobrancaAtiva"));

            var host = "srvoradev03";
            var port = 1521;
            var sid = "gtdev";
            var user = "nilton";
            var pass = "nildti2006";
            var conn = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME={sid})));User Id={user};Password={pass};";
            services.AddDbContext<CobrancaAtivaDbContext>(options =>
                options.UseOracle(conn));

            return services;
        }
    }
}
