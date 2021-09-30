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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CobrancaAtivaDbContext).Assembly);
        }
    }
}