using Microsoft.EntityFrameworkCore;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Context
{
    public class CobrancaAtivaDbContext : DbContext
    {

        public CobrancaAtivaDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<EmpresaParceiraModel> EmpresaParceira { get; set; }
        public DbSet<ContatoEmpresaParceiraModel> ContatoEmpresaParceira { get; set; }
        public DbSet<EnderecoEmpresaParceiraModel> EnderecoEmpresaParceira { get; set; }
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
        public DbSet<RegraNegociacaoTipoPagamentoModel> RegraNegociacaoTipoPagamento { get; set; }
        public DbSet<RegraNegociacaoTipoTituloModel> RegraNegociacaoTipoTitulo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CobrancaAtivaDbContext).Assembly);
        }
    }
}