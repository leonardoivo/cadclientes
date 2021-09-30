﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class HonorarioEmpresaParceiraMapping : IEntityTypeConfiguration<HonorarioEmpresaParceiraModel>
    {
        public void Configure(EntityTypeBuilder<HonorarioEmpresaParceiraModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_HONORARIO");

            builder.Property(ep => ep.PercentualNegociacaoEmpresaParceira)
              .HasColumnName("PERC_EMPR_COBR");

            builder.Property(ep => ep.ValorNegociacaoEmpresaParceira)
              .HasColumnName("VALOR_EMPR_COBR");

            builder.Property(ep => ep.PercentualNegociacaoInstituicaoEnsino)
              .HasColumnName("PERC_INSTITUICAO");

            builder.Property(ep => ep.ValorNegociacaoInstituicaoEnsino)
              .HasColumnName("VALOR_INSTITUICAO");

            builder.Property(ep => ep.PercentualCobrancaIndevida)
              .HasColumnName("PERC_COBRANCA_INDEVIDA");

            builder.Property(ep => ep.ValorCobrancaIndevida)
              .HasColumnName("VALOR_COBRANCA_INDEVIDA");

            builder.Property(ep => ep.Informacao)
              .HasColumnName("INFORMACOES");

            builder.Property(ep => ep.InstituicaoId)
             .HasColumnName("COD_INSTITUICAO");

            builder.Property(ep => ep.ModalidadeId)
             .HasColumnName("COD_MODALIDADE");

            builder.Property(ep => ep.EmpresaParceiraId)
             .HasColumnName("COD_EMPRESA");

            builder.HasOne(c => c.EmpresaParceira)
                .WithMany(e => e.Honorarios)
                .HasForeignKey(c => c.EmpresaParceiraId)
                .HasConstraintName("FK_HONORARIO_COD_EMPRESA");

            builder.HasOne(c => c.Instituicao)
                .WithMany(e => e.Honorarios)
                .HasForeignKey(c => c.InstituicaoId);

            builder.HasOne(c => c.Modalidade)
                .WithMany(e => e.Honorarios)
                .HasForeignKey(c => c.ModalidadeId);

            builder.ToTable("HONORARIOS");
        }
    }
}