using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repositorios
            services.AddScoped<IEmpresaParceiraRepository, EmpresaParceiraRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();
            services.AddScoped<IModalidadeRepository, ModalidadeRepository>();
            services.AddScoped<IHonorarioEmpresaParceiraRepository, HonorarioEmpresaParceiraRepository>();
            #endregion

            #region Services
            services.AddScoped<IEmpresaParceiraService, EmpresaParceiraService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IInstituicaoService, InstituicaoService>();
            services.AddScoped<IModalidadeService, ModalidadeService>();
            services.AddScoped<IHonorarioEmpresaParceiraService, HonorarioEmpresaParceiraService>();
            #endregion

            
            services.AddDbContext<CobrancaAtivaDbContext>(options =>
                options.UseOracle(configuration.GetConnectionString("Empresas")));

            return services;
        }
    }
}
