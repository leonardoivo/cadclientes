using Microsoft.EntityFrameworkCore;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Context
{
    public class CobrancaAtivaDbContext : DbContext
    {

        public CobrancaAtivaDbContext(DbContextOptions<CobrancaAtivaDbContext> options) : base(options)
        { }

        public DbSet<BancoModel> Banco { get; set; }
        public DbSet<EmpresaParceiraModel> EmpresaParceira { get; set; }
        public DbSet<ContatoEmpresaParceiraModel> ContatoEmpresaParceira { get; set; }
        public DbSet<ConflitoModel> ConflitoModel { get; set; }
        public DbSet<HonorarioEmpresaParceiraModel> HonorarioEmpresaParceiras { get; set; }
        public DbSet<HonorarioFaixaEmpresaParceiraModel> HonorarioFaixaEmpresaParceiras { get; set; }
        public DbSet<EnderecoEmpresaParceiraModel> EnderecoEmpresaParceira { get; set; }
        public DbSet<ContaBancariaEmpresaParceiraModel> ContaBancariaEmpresaParceira { get; set; }
        public DbSet<InstituicaoModel> Instituicao { get; set; }
        public DbSet<ModalidadeModel> Modalidade { get; set; }
        public DbSet<CursoModel> Curso { get; set; }
        public DbSet<SemestreModel> Semestre { get; set; }
        public DbSet<InstituicaoModalidadeModel> InstituicaoModalidade { get; set; }
        public DbSet<SituacaoAlunoModel> SituacaoAluno { get; set; }
        public DbSet<TipoPagamentoModel> TipoPagamento { get; set; }
        public DbSet<TipoTituloModel> TipoTitulo { get; set; }
        public DbSet<RegraNegociacaoModel> RegraNegociacao { get; set; }
        public DbSet<RegraNegociacaoCursoModel> RegraNegociacaoCurso { get; set; }
        public DbSet<RegraNegociacaoTituloAvulsoModel> RegraNegociacaoTituloAvulso { get; set; }
        public DbSet<RegraNegociacaoSituacaoAlunoModel> RegraNegociacaoSituacaoAluno { get; set; }
        public DbSet<RegraNegociacaoTipoTituloModel> RegraNegociacaoTipoTitulo { get; set; }
        public DbSet<ParametroEnvioModel> ParametroEnvio { get; set; }
        public DbSet<ParametroEnvioCursoModel> ParametroEnvioCurso { get; set; }
        public DbSet<ParametroEnvioSituacaoAlunoModel> ParametroEnvioSituacaoAluno { get; set; }
        public DbSet<ParametroEnvioTipoTituloModel> ParametroEnvioTipoTitulo { get; set; }
        public DbSet<ParametroEnvioTituloAvulsoModel> ParametroEnvioTituloAvulso { get; set; }


        public DbSet<AcordosCobrancasModel> AcordosCobrancasModel { get; set; }
        public DbSet<ParcelasTitulosModel> ParcelasTitulosModel { get; set; }
        public DbSet<ParcelasAcordoModel> ParcelasAcordoModel { get; set; }
        public DbSet<ItensBaixaTipo1Model> ItensBaixaTipo1 { get; set; }
        public DbSet<ItensBaixaTipo2Model> ItensBaixaTipo2 { get; set; }
        public DbSet<ItensBaixaTipo3Model> ItensBaixaTipo3 { get; set; }
        public DbSet<ArquivoLayoutModel> ArquivoLayout { get; set; }
        public DbSet<ErrosLayoutModel> ErrosLayout{ get; set; }
        
        public DbSet<BaixasCobrancasModel> BaixasCobrancas { get; set; }

        /// <summary>
        /// Model Ficticio para validação da existencia de um aluno
        /// </summary>
        public DbSet<MatriculaAlunoExisteModel> MatriculaAlunoExisteModel { get; set; }

        /// <summary>
        /// Model Ficticio para validação da existencia de pagamento de parcela
        /// </summary>
        public DbSet<ParcelaPagaAlunoInstituicaoModel> ParcelaPagaAlunoInstituicaoModel { get; set; }

        /// <summary>
        /// Model Ficticio para busca do Id Aluno
        /// </summary>
        public DbSet<IdAlunoModel> IdAlunoModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CobrancaAtivaDbContext).Assembly);
        }
    }
}