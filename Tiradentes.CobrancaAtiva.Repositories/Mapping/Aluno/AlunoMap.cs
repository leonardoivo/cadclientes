using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiradentes.CobrancaAtiva.Repositories.Mapping.Aluno
{
    public class AlunoMap : IEntityTypeConfiguration<Models.Aluno.AlunoModel>
    {
        public void Configure(EntityTypeBuilder<Models.Aluno.AlunoModel> entity)
        {
            entity.ToTable("aluno");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Nome)
                .HasColumnName("nome");
        }
    }
}
