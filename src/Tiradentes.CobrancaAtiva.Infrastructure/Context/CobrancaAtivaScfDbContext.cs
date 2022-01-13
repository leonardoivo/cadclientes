using Microsoft.EntityFrameworkCore;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Context
{
    public class CobrancaAtivaScfDbContext : DbContext
    {

        public CobrancaAtivaScfDbContext(DbContextOptions<CobrancaAtivaScfDbContext> options) : base(options)
        { }

        public DbSet<GeracaoCobrancasModel> GeracaoCobrancas { get; set; }
        public DbSet<ItensGeracaoModel> ItensGeracao { get; set; }
        public DbSet<ArquivoCobrancasModel> ArquivoCobrancas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SCF");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CobrancaAtivaScfDbContext).Assembly);
        }
    }
}