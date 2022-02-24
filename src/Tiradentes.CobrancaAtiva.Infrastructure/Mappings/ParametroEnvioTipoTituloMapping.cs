﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ParametroEnvioTipoTituloMapping : IEntityTypeConfiguration<ParametroEnvioTipoTituloModel>
    {
        public void Configure(EntityTypeBuilder<ParametroEnvioTipoTituloModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_PARAMETRO_ENVIO_TIPO_TITULO");

            builder.Property(ep => ep.TipoTituloId)
              .HasColumnName("COD_TIPO_TITULO");

            builder.Property(ep => ep.ParametroEnvioId)
              .HasColumnName("COD_PARAMETRO_ENVIO");

            builder.HasOne(c => c.TipoTitulo)
                .WithMany(e => e.ParametroEnvioTipoTitulo)
                .HasForeignKey(c => c.TipoTituloId);

            builder.HasOne(c => c.ParametroEnvio)
                .WithMany(e => e.ParametroEnvioTipoTitulo)
                .HasForeignKey(c => c.ParametroEnvioId);

            builder.ToTable("PARAMETRO_ENVIO_TIPO_TITULO", "APP_COBRANCA");
        }
    }
}
