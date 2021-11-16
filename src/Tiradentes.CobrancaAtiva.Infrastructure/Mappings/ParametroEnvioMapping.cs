using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ParametroEnvioMapping : IEntityTypeConfiguration<ParametroEnvioModel>
    {
        public void Configure(EntityTypeBuilder<ParametroEnvioModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_PARAMETRO_ENVIO");

            builder.Property(ep => ep.EmpresaParceiraId)
              .HasColumnName("COD_EMPRESA");

            builder.Property(ep => ep.DiaEnvio)
              .HasColumnName("DIA_ENVIO");

            builder.Property(ep => ep.Status)
              .HasColumnName("STATUS")
              .HasColumnType("NUMBER(1)");

            builder.Property(ep => ep.InadimplenciaInicial)
              .HasColumnName("INADIMPLENCIA_INICIAL")
              .HasColumnType("DATE");

            builder.Property(ep => ep.InadimplenciaFinal)
              .HasColumnName("INADIMPLENCIA_FINAL")
              .HasColumnType("DATE");

            builder.Property(ep => ep.ValidadeInicial)
              .HasColumnName("VALIDADE_INICIAL")
              .HasColumnType("DATE");

            builder.Property(ep => ep.ValidadeFinal)
              .HasColumnName("VALIDADE_FINAL")
              .HasColumnType("DATE");

            builder.HasOne(im => im.EmpresaParceira)
                .WithMany(m => m.ParametroEnvios)
                .HasForeignKey(im => im.EmpresaParceiraId);

            builder.HasMany(c => c.ParametroEnvioCurso)
               .WithOne(e => e.ParametroEnvio);

            builder.HasMany(c => c.ParametroEnvioSituacaoAluno)
              .WithOne(e => e.ParametroEnvio);

            builder.HasMany(c => c.ParametroEnvioTipoTitulo)
              .WithOne(e => e.ParametroEnvio);

            builder.HasMany(c => c.ParametroEnvioTituloAvulso)
              .WithOne(e => e.ParametroEnvio);

            builder.ToTable("PARAMETRO_ENVIO");
        }
    }
}

