using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class RegraNegociacaoSemestreMapping : IEntityTypeConfiguration<RegraNegociacaoTituloAvulsoModel>
    {
        public void Configure(EntityTypeBuilder<RegraNegociacaoTituloAvulsoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_REGRA_NEGOCIACAO_TITULO_AVULSO");

            builder.Property(ep => ep.TituloAvulsoId)
              .HasColumnName("COD_TITULO_AVULSO");

            builder.Property(ep => ep.RegraNegociacaoId)
              .HasColumnName("COD_REGRA_NEGOCIACAO");

            builder.HasOne(c => c.TituloAvulso)
                .WithMany(e => e.RegraNegociacaoTituloAvulso)
                .HasForeignKey(c => c.TituloAvulsoId);

            builder.HasOne(c => c.RegraNegociacao)
                .WithMany(e => e.RegraNegociacaoTituloAvulso)
                .HasForeignKey(c => c.RegraNegociacaoId);

            builder.ToTable("REGRA_NEGOCIACAO_TITULO_AVULSO");
        }
    }
}
