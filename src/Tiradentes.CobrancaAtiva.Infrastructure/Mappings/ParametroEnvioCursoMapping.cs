using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ParametroEnvioCursoMapping : IEntityTypeConfiguration<ParametroEnvioCursoModel>
    {
        public void Configure(EntityTypeBuilder<ParametroEnvioCursoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_PARAMETRO_ENVIO_CURSO");

            builder.Property(ep => ep.CursoId)
              .HasColumnName("COD_CURSO");

            builder.Property(ep => ep.ParametroEnvioId)
              .HasColumnName("COD_PARAMETRO_ENVIO");

            builder.HasOne(c => c.Curso)
                .WithMany(e => e.ParametroEnvioCurso)
                .HasForeignKey(c => c.CursoId);

            builder.HasOne(c => c.ParametroEnvio)
                .WithMany(e => e.ParametroEnvioCurso)
                .HasForeignKey(c => c.ParametroEnvioId);

            builder.ToTable("PARAMETRO_ENVIO_CURSO", "APP_COBRANCA");
        }
    }
}
