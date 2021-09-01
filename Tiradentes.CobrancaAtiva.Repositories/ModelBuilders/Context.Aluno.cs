using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiradentes.CobrancaAtiva.Repositories
{
    public partial class TesteContext
    {
        public DbSet<Models.Aluno.AlunoModel> Alunos { get; set; }

        private void CreatingAlunoContext(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Mapping.Aluno.AlunoMap());
        }
    }
}
