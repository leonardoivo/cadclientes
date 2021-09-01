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
            #endregion
            services.AddScoped<IEmpresaParceiraService, EmpresaParceiraService>();
            #region Services

            #endregion

            services.AddDbContext<CobrancaAtivaDbContext>(options => 
                options.UseInMemoryDatabase("CobrancaAtiva"));

            return services;
        }
    }
}
