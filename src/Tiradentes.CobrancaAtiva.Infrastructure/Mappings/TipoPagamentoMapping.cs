using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class TipoPagamentoMapping : IEntityTypeConfiguration<TipoPagamentoModel>
    {
        public void Configure(EntityTypeBuilder<TipoPagamentoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_TIPO_PAGAMENTO");

            builder.Property(ep => ep.TipoPagamento)
             .HasColumnName("DESCRICAO_TIPO_PAGAMENTO");

            builder.ToTable("TIPO_PAGAMENTO", "APP_COBRANCA");
        }
    }
}
