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
              .HasColumnName("NUM_ACORDO");

            builder.Property(ep => ep.Matricula)
                .HasColumnName("MATRICULA");

            builder.Property(ep => ep.Periodo)
                .HasColumnName("PERIODO");

            builder.Property(ep => ep.PeriodoOutros)
                .HasColumnName("PERIODO_OUTROS");

            builder.Property(ep => ep.Parcela)
                .HasColumnName("PARCELA");

            builder.Property(ep => ep.CnpjEmpresaCobranca)
                .HasColumnName("CNPJ_EMPRESA_COBRANCA");

            builder.Property(ep => ep.DataBaixa)
              .HasColumnName("DAT_BAIXA");

            builder.Property(ep => ep.DataEnvio)
              .HasColumnName("DAT_ENVIO");

            builder.Property(ep => ep.DataVencimento)
                .HasColumnName("DAT_VENC");

            builder.Property(ep => ep.Valor)
                 .HasColumnName("VALOR");

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
