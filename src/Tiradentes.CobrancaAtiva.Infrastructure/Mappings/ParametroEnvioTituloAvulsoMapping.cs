using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ParametroEnvioTituloAvulsoMapping : IEntityTypeConfiguration<ParametroEnvioTituloAvulsoModel>
    {
        public void Configure(EntityTypeBuilder<ParametroEnvioTituloAvulsoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_PARAMETRO_ENVIO_TITULO_AVULSO");

            builder.Property(ep => ep.TituloAvulsoId)
              .HasColumnName("COD_TITULO_AVULSO");

            builder.Property(ep => ep.ParametroEnvioId)
              .HasColumnName("COD_PARAMETRO_ENVIO");

            builder.HasOne(c => c.TituloAvulso)
                .WithMany(e => e.ParametroEnvioTituloAvulso)
                .HasForeignKey(c => c.TituloAvulsoId);

            builder.HasOne(c => c.ParametroEnvio)
                .WithMany(e => e.ParametroEnvioTituloAvulso)
                .HasForeignKey(c => c.ParametroEnvioId);

            builder.ToTable("PARAMETRO_ENVIO_TITULO_AVULSO", "APP_COBRANCA");
        }
    }
}
