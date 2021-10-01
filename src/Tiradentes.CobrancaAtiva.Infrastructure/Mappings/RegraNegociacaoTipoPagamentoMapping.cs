using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class RegraNegociacaoTipoPagamentoMapping : IEntityTypeConfiguration<RegraNegociacaoTipoPagamentoModel>
    {
        public void Configure(EntityTypeBuilder<RegraNegociacaoTipoPagamentoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_REGRA_NEGOCIACAO_CURSO");

            builder.Property(ep => ep.TipoPagamentoId)
              .HasColumnName("COD_TIPO_PAGAMENTO");

            builder.Property(ep => ep.RegraNegociacaoId)
              .HasColumnName("COD_REGRA_NEGOCIACAO");

            builder.HasOne(c => c.TipoPagamento)
                .WithMany(e => e.RegraNegociacaoTipoPagamento)
                .HasForeignKey(c => c.TipoPagamentoId);

            builder.HasOne(c => c.RegraNegociacao)
                .WithMany(e => e.RegraNegocicaoTipoPagamento)
                .HasForeignKey(c => c.RegraNegociacaoId);

            builder.ToTable("REGRA_NEGOCIACAO_TIPO_PAGAMENTO");
        }
    }
}
