using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ParcelasTitulosMapping : IEntityTypeConfiguration<ParcelasTitulosModel>
    {
        public void Configure(EntityTypeBuilder<ParcelasTitulosModel> builder)
        {
            builder.Ignore(ep => ep.Id);

            builder.HasKey(ep => new { ep.NumeroAcordo, ep.Matricula, ep.Periodo, ep.Parcela });

            builder.Property(ep => ep.NumeroAcordo)
              .HasColumnName("NUM_ACORDO")
              .HasColumnType("NUMBER(10)");

            builder.Property(ep => ep.Matricula)
                .HasColumnName("MATRICULA")
                .HasColumnType("NUMBER(11)");

            builder.Property(ep => ep.Periodo)
                .HasColumnName("PERIODO")
                .HasColumnType("NUMBER(5)");

            builder.Property(ep => ep.PeriodoChequeDevolvido)
                .HasColumnName("PERIODO_CHEQUE_DEVOLVIDO")
                .HasColumnType("NUMBER(5)");

            builder.Property(ep => ep.Parcela)
                .HasColumnName("PARCELA")
                .HasColumnType("NUMBER(3)");

            builder.Property(ep => ep.CnpjEmpresaCobranca)
                .HasColumnName("CNPJ_EMPRESA_COBRANCA");

            builder.Property(ep => ep.DataBaixa)
              .HasColumnName("DAT_BAIXA");

            builder.Property(ep => ep.DataEnvio)
              .HasColumnName("DAT_ENVIO");

            builder.Property(ep => ep.DataVencimento)
                .HasColumnName("DAT_VENC");

            builder.Property(ep => ep.Valor)
                 .HasColumnName("VALOR")
                 .HasColumnType("NUMBER(15,2)");

            builder.Property(ep => ep.Sistema)
                 .HasColumnName("SISTEMA")
                 .HasColumnType("CHAR(1)");

            builder.Property(ep => ep.TipoInadimplencia)
                 .HasColumnName("TIPO_INADIMPLENCIA")
                 .HasColumnType("CHAR(1)");



            builder.ToTable("PARCELAS_TITULOS", "SCF");
        }
    }
}
