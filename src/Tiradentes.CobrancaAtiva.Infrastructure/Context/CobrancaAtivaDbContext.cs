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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CobrancaAtivaDbContext).Assembly);
        }
    }
}