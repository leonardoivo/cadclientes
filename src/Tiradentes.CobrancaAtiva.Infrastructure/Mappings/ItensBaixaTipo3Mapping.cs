using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ItensBaixaTipo3Mapping : IEntityTypeConfiguration<ItensBaixaTipo3Model>
    {
        public void Configure(EntityTypeBuilder<ItensBaixaTipo3Model> builder)
        {
            builder.Ignore(ep => ep.Id);

            builder.HasKey(ep => new { ep.DataBaixa, ep.Sequencia });

            builder.Property(ep => ep.DataBaixa)
              .HasColumnName("DAT_BAIXA");

            builder.Property(ep => ep.Sequencia)
                .HasColumnName("SEQ");

            builder.Property(ep => ep.CodigoErro)
              .HasColumnName("COD_ERRO");

            builder.Property(ep => ep.NumeroLinha)
               .HasColumnName("NUM_LINHA");

            builder.Property(ep => ep.NumeroAcordo)
               .HasColumnName("NUM_ACORDO");

            builder.Property(ep => ep.Matricula)
               .HasColumnName("MATRICULA")
               .HasColumnType("NUMBER(11)");

            builder.Property(ep => ep.Parcela)
              .HasColumnName("PARCELA");

            builder.Property(ep => ep.DataPagamento)
               .HasColumnName("DAT_PGTO");

            builder.Property(ep => ep.ValorPago)
               .HasColumnName("VALOR_PAGO");

            builder.Property(ep => ep.CnpjEmpresaCobranca)
                .HasColumnName("CNPJ_EMPRESA_COBRANCA");

            builder.Property(ep => ep.Sistema)
                .HasColumnName("SISTEMA")
                .HasColumnType("CHAR(1)");

            builder.Property(ep => ep.TipoInadimplencia)
                .HasColumnName("TIPO_INADIMPLENCIA")
                .HasColumnType("CHAR(1)");

            builder.Property(ep => ep.Tipo_Pagamento)
                .HasColumnName("TIPO_PGTO")
                .HasColumnType("CHAR(1)");

            builder.Property(ep => ep.SituacaoAluno)
                .HasColumnName("STA_ALU ")
                .HasColumnType("CHAR(1)");

            builder.ToTable("ITENS_BAIXAS_TIPO3", "SCF");
        }
    }
}
