﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class SituacaoAlunoMapping : IEntityTypeConfiguration<SituacaoAlunoModel>
    {
        public void Configure(EntityTypeBuilder<SituacaoAlunoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_SITUACAO_ALUNO");

            builder.Property(ep => ep.Situacao)
             .HasColumnName("DESCRICAO_SITUACAO");

            builder.HasMany(c => c.RegraNegociacaoSituacaoAluno)
               .WithOne(e => e.SituacaoAluno);

            builder.ToTable("SITUACOES_ALUNO");
        }
    }
}
