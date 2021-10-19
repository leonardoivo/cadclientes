﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class RegraNegociacaoMapping : IEntityTypeConfiguration<RegraNegociacaoModel>
    {
        public void Configure(EntityTypeBuilder<RegraNegociacaoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_REGRA_NEGOCIACAO");

            builder.Property(ep => ep.InstituicaoId)
              .HasColumnName("COD_INSTITUICAO");

            builder.Property(ep => ep.ModalidadeId)
              .HasColumnName("COD_MODALIDADE");

            builder.Property(ep => ep.PercentJurosMulta)
              .HasColumnName("PERCENT_JUROS_MULTA")
              .HasColumnType("NUMBER(3,5)");

            builder.Property(ep => ep.PercentValor)
              .HasColumnName("PERCENT_VALOR")
              .HasColumnType("NUMBER(3,5)");

            builder.Property(ep => ep.Status)
              .HasColumnName("STATUS")
              .HasColumnType("NUMBER(1)");

            builder.Property(ep => ep.MesAnoInicial)
              .HasColumnName("MESANO_INICIAL")
              .HasColumnType("DATE");

            builder.Property(ep => ep.MesAnoFinal)
              .HasColumnName("MESANO_FINAL")
              .HasColumnType("DATE");

            builder.Property(ep => ep.ValidadeInicial)
              .HasColumnName("VALIDADE_INICIAL")
              .HasColumnType("DATE");

            builder.Property(ep => ep.ValidadeFinal)
              .HasColumnName("VALIDADE_FINAL")
              .HasColumnType("DATE");

            builder.HasOne(im => im.Instituicao)
                .WithMany(m => m.RegraNegociacao)
                .HasForeignKey(im => im.InstituicaoId);

            builder.HasOne(im => im.Modalidade)
                .WithMany(m => m.RegraNegociacao)
                .HasForeignKey(im => im.ModalidadeId);

            builder.HasMany(c => c.RegraNegociacaoCurso)
               .WithOne(e => e.RegraNegociacao);

            builder.HasMany(c => c.RegraNegociacaoTituloAvulso)
              .WithOne(e => e.RegraNegociacao);

            builder.HasMany(c => c.RegraNegociacaoSituacaoAluno)
              .WithOne(e => e.RegraNegociacao);

            builder.HasMany(c => c.RegraNegociacaoTipoPagamento)
              .WithOne(e => e.RegraNegociacao);

            builder.HasMany(c => c.RegraNegociacaoTipoTitulo)
              .WithOne(e => e.RegraNegociacao);

            builder.ToTable("REGRA_NEGOCIACAO");
        }
    }
}

