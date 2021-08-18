using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Infraestrutura.BancoDados.ContextoBanco
{
    public class TesteContext : DbContext, IUnitOfWork
    {
        public TesteContext() { }
        public TesteContext(DbContextOptions<TesteContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseInMemoryDatabase("bdteste");
            }
        }

        public DbSet<Teste.Dominio.Entidades.Teste> Testes { get; set; }

        public void Save()
        {
            base.SaveChanges();
        }
    }
}
