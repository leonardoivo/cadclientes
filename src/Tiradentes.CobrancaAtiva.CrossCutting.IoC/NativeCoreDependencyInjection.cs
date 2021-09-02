using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Services;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

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

            services.AddDbContext<CobrancaAtivaDbContext>(options =>
                options.UseInMemoryDatabase("CobrancaAtiva"));

            //services.AddDbContext<CobrancaAtivaDbContext>(options =>
            //    options.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=srvoradev03)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=gtdev)));User Id=nilton;Password=nildti2006;"));

            return services;
        }
    }
}
