using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ParametroEnvioModalidadeMapping : IEntityTypeConfiguration<ParametroEnvioModalidadeModel>
    {
        public void Configure(EntityTypeBuilder<ParametroEnvioModalidadeModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_PARAMETRO_ENVIO_MODALIDADE");

            builder.Property(ep => ep.ModalidadeId)
              .HasColumnName("COD_MODALIDADE");

            builder.Property(ep => ep.ParametroEnvioId)
              .HasColumnName("COD_PARAMETRO_ENVIO");

            builder.HasOne(c => c.Modalidade)
                .WithMany(e => e.ParametroEnvioModalidade)
                .HasForeignKey(c => c.ModalidadeId);

            builder.HasOne(c => c.ParametroEnvio)
                .WithMany(e => e.ParametroEnvioModalidade)
                .HasForeignKey(c => c.ParametroEnvioId);

            builder.ToTable("PARAMETRO_ENVIO_MODALIDADE");
        }
    }
}
