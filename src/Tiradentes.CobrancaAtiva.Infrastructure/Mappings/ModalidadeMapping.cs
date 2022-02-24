﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class ModalidadeMapping : IEntityTypeConfiguration<ModalidadeModel>
    {
        public void Configure(EntityTypeBuilder<ModalidadeModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_MODALIDADE");

            builder.Property(ep => ep.Modalidade)
             .HasColumnName("MODALIDADE");

            builder.Property(ep => ep.CodigoMagister)
             .HasColumnName("COD_MAGISTER");

            builder.HasMany(m => m.InstituicoesModalidades)
                .WithOne(im => im.Modalidade);

            builder.HasMany(c => c.RegraNegociacao)
               .WithOne(e => e.Modalidade);

            builder.ToTable("MODALIDADES", "APP_COBRANCA");
        }
    }
}
