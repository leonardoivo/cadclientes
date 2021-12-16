using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class ConflitoMapping : IEntityTypeConfiguration<ConflitoModel>
    {
        public void Configure(EntityTypeBuilder<ConflitoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.EmpresaParceiraTentativa)
              .HasColumnName("EmpresaParceiraTentativa");

            builder.Property(ep => ep.EmpresaParceiraEnvio)
             .HasColumnName("EmpresaParceiraTentativa");

            builder.Property(ep => ep.Matricula)
             .HasColumnName("Matricula");

            builder.Property(ep => ep.ModalidadeEnsino)
            .HasColumnName("ModalidadeEnsino");

            builder.Property(ep => ep.Nomealuno)
           .HasColumnName("Nomealuno");

            builder.Property(ep => ep.NomeLote)
           .HasColumnName("NomeLote");

            builder.Property(ep => ep.ParcelaConflito)
           .HasColumnName("ParcelaConflito");

            builder.Property(ep => ep.SituacaoConflito)
           .HasColumnName("SituacaoConflito");

            builder.Property(ep => ep.ValorConflito)
           .HasColumnName("ValorConflito");

            builder.ToTable("Conflito");
        }
    }
}
