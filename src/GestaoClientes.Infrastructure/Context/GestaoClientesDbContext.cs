using Microsoft.EntityFrameworkCore;
using GestaoClientes.Domain.Models;

namespace GestaoClientes.Infrastructure.Context
{
    public class GestaoClientesDbContext : DbContext
    {

        public GestaoClientesDbContext(DbContextOptions<GestaoClientesDbContext> options) : base(options)
        { }

       
        public DbSet<ClienteModel> Cliente { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoClientesDbContext).Assembly);
        }
    }
}