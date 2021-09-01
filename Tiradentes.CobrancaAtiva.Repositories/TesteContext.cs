using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Repositories
{
    public partial class TesteContext : DbContext
    {
        public TesteContext() { }
        public TesteContext(DbContextOptions<TesteContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Model Builder

            CreatingAlunoContext(modelBuilder);

            #endregion Model Builder

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseInMemoryDatabase("bdteste");
            }
        }

        public void Save()
        {
            base.SaveChanges();
        }

        public void DetachAll()
        {
            foreach (var entityEntry in ChangeTracker.Entries().ToArray())
            {
                if (entityEntry.Entity != null)
                {
                    entityEntry.State = EntityState.Detached;
                }
            }
        }
    }
}
