using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ParcelasAcordoMapping : IEntityTypeConfiguration<ParcelasAcordoModel>
    {
        public void Configure(EntityTypeBuilder<ParcelasAcordoModel> builder)
        {
            builder.Ignore(ep => ep.Id);

            builder.HasKey(ep => new { ep.NumeroAcordo, ep.Parcela });

            builder.Property(ep => ep.NumeroAcordo)
                .HasColumnName("NUM_ACORDO");

            builder.Property(ep => ep.Parcela)
                .HasColumnName("PARCELA");

            builder.Property(ep => ep.DataBaixa)
                .HasColumnName("DAT_BAIXA");

            builder.Property(ep => ep.DataVencimento)
                .HasColumnName("DAT_VENC");

            builder.Property(ep => ep.DataPagamento)
                .HasColumnName("DAT_PGTO");

            builder.Property(ep => ep.DataBaixaPagamento)
                .HasColumnName("DAT_BAIXA_PGTO");

            builder.Property(ep => ep.Valor)
                .HasColumnName("VALOR");

            builder.Property(ep => ep.ValorPago)
                .HasColumnName("VALOR_PAGO");

            builder.Property(ep => ep.CodigoBanco)
                .HasColumnName("COD_BANCO");

            builder.Property(ep => ep.CnpjEmpresaCobranca)
                .HasColumnName("CNPJ_EMPRESA_COBRANCA");

            builder.Property(ep => ep.Sistema)
                .HasColumnName("SISTEMA")
                .HasColumnType("CHAR(1)");

            builder.Property(ep => ep.TipoInadimplencia)
                .HasColumnName("TIPO_INADIMPLENCIA")
                .HasColumnType("CHAR(1)");

            builder.ToTable("PARCELAS_ACORDO", "SCF");

        }
    }
}
