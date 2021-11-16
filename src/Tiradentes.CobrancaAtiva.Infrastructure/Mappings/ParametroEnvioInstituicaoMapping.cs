using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ParametroEnvioInstituicaoMapping : IEntityTypeConfiguration<ParametroEnvioInstituicaoModel>
    {
        public void Configure(EntityTypeBuilder<ParametroEnvioInstituicaoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_PARAMETRO_ENVIO_INSTITUICAO");

            builder.Property(ep => ep.InstituicaoId)
              .HasColumnName("COD_INSTITUICAO");

            builder.Property(ep => ep.ParametroEnvioId)
              .HasColumnName("COD_PARAMETRO_ENVIO");

            builder.HasOne(c => c.Instituicao)
                .WithMany(e => e.ParametroEnvioInstituicao)
                .HasForeignKey(c => c.InstituicaoId);

            builder.HasOne(c => c.ParametroEnvio)
                .WithMany(e => e.ParametroEnvioInstituicao)
                .HasForeignKey(c => c.ParametroEnvioId);

            builder.ToTable("PARAMETRO_ENVIO_INSTITUICAO");
        }
    }
}
