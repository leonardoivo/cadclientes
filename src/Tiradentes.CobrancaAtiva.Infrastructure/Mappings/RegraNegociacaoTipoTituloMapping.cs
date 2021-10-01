using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class RegraNegociacaoTipoTituloMapping : IEntityTypeConfiguration<RegraNegociacaoTipoTituloModel>
    {
        public void Configure(EntityTypeBuilder<RegraNegociacaoTipoTituloModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_REGRA_NEGOCIACAO_CURSO");

            builder.Property(ep => ep.TipoTituloId)
              .HasColumnName("COD_TIPO_TITULO");

            builder.Property(ep => ep.RegraNegociacaoId)
              .HasColumnName("COD_REGRA_NEGOCIACAO");

            builder.HasOne(c => c.TipoTitulo)
                .WithMany(e => e.RegraNegociacaoTipoTitulo)
                .HasForeignKey(c => c.TipoTituloId);

            builder.HasOne(c => c.RegraNegociacao)
                .WithMany(e => e.RegraNegociacaoTipoTitulo)
                .HasForeignKey(c => c.RegraNegociacaoId);

            builder.ToTable("REGRA_NEGOCIACAO_TIPO_TITULO");
        }
    }
}
