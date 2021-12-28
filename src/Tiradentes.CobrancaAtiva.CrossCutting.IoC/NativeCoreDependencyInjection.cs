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
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<ISemestreRepository, SemestreRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();
            services.AddScoped<IModalidadeRepository, ModalidadeRepository>();
            services.AddScoped<ISituacaoAlunoRepository, SituacaoAlunoRepository>();
            services.AddScoped<ITipoPagamentoRepository, TipoPagamentoRepository>();
            services.AddScoped<ITipoTituloRepository, TipoTituloRepository>();
            services.AddScoped<IHonorarioEmpresaParceiraRepository, HonorarioEmpresaParceiraRepository>();
            services.AddScoped<IRegraNegociacaoRepository, RegraNegociacaoRepository>();
            services.AddScoped<ITituloAvulsoRepository, TituloAvulsoRepository>();
            services.AddScoped<IParametroEnvioRepository, ParametroEnvioRepository>();
            services.AddScoped<IBancoRepository, BancoRepository>();
            services.AddScoped<IAlunosInadimplentesRepository, AlunosInadimplentesRepository>();
            services.AddScoped<IGeracaoCobrancasRepository, GeracaoCobrancasRepository>();
            services.AddScoped<IArquivoCobrancasRepository, ArquivoCobrancasRepository>();
            services.AddScoped<IItensGeracaoRepository, ItensGeracaoRepository>();
            services.AddScoped<IApplicationErrorRepository, ApplicationErrorRepository>();
            services.AddScoped<IConflitoRepository, ConflitoRepository>();
            services.AddScoped<IConflitoDetalheRepository, ConflitoDetalheRepository>();
            #endregion

            #region Services
            services.AddScoped<IEmpresaParceiraService, EmpresaParceiraService>();
            services.AddScoped<ICursoService, CursoService>();
            services.AddScoped<ISemestreService, SemestreService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IInstituicaoService, InstituicaoService>();
            services.AddScoped<IModalidadeService, ModalidadeService>();
            services.AddScoped<ISituacaoAlunoService, SituacaoAlunoService>();
            services.AddScoped<ITipoTituloService, TipoTituloService>();
            services.AddScoped<ITipoPagamentoService, TipoPagamentoService>();
            services.AddScoped<IHonorarioEmpresaParceiraService, HonorarioEmpresaParceiraService>();
            services.AddScoped<IBancoService, BancoService>();
            services.AddScoped<IRegraNegociacaoService, RegraNegociacaoService>();
            services.AddScoped<ITituloAvulsoService, TituloAvulsoService>();
            services.AddScoped<IParametroEnvioService, ParametroEnvioService>();
            services.AddScoped<IApplicationErrorService, ApplicationErrorService>();
            services.AddScoped<IConflitoService, ConflitoService>();
            services.AddScoped<IConflitoDetalheService, ConflitoDetalheService>();
            #endregion

            services.AddScoped<MongoContext>();
            services.AddDbContext<CobrancaAtivaDbContext>(options =>
                options.UseOracle(configuration.GetConnectionString("Empresas"))); 
            services.AddDbContext<CobrancaAtivaScfDbContext>(options =>
                options.UseOracle(configuration.GetConnectionString("EmpresasScf")));

            return services;
        }
    }
}
