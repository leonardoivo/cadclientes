
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class BaixasCobrancasMapping : IEntityTypeConfiguration<BaixasCobrancasModel>
    {
        public void Configure(EntityTypeBuilder<BaixasCobrancasModel> builder)
        {
            builder.HasKey(ep => ep.DataBaixa);

            builder.Property(ep => ep.DataBaixa)
               .HasColumnName("DAT_BAIXA");

            builder.Property(ep => ep.UserName)
               .HasColumnName("USERNAME");

            builder.Property(ep => ep.Etapa)
                .HasColumnName("ETAPA");

            builder.Property(ep => ep.QuantidadeTipo1)
                .HasColumnName("QTD_TIPO1");

            builder.Property(ep => ep.QuantidadeTipo2)
                .HasColumnName("QTD_TIPO2");

            builder.Property(ep => ep.QuantidadeTipo3)
                .HasColumnName("QTD_TIPO3");

            builder.Property(ep => ep.QuantidadeErrosTipo1)
                .HasColumnName("QTD_ERROS_TIPO1");

            builder.Property(ep => ep.QuantidadeErrosTipo2)
                .HasColumnName("QTD_ERROS_TIPO2");

            builder.Property(ep => ep.QuantidadeErrosTipo3)
                .HasColumnName("QTD_ERROS_TIPO3");

            builder.Property(ep => ep.TotalTipo1)
                .HasColumnName("TOTAL_TIPO1");

            builder.Property(ep => ep.TotalTipo2)
                .HasColumnName("TOTAL_TIPO2");

            builder.Property(ep => ep.TotalTipo3)
                .HasColumnName("TOTAL_TIPO3");

            builder.Property(ep => ep.TotalErrosTipo1)
                .HasColumnName("TOTAL_ERROS_TIPO1");

            builder.Property(ep => ep.TotalErrosTipo2)
                .HasColumnName("TOTAL_ERROS_TIPO2");

            builder.Property(ep => ep.TotalErrosTipo3)
                .HasColumnName("TOTAL_ERROS_TIPO3");

            builder.ToTable("BAIXAS_COBRANCAS");
        }
    }
}
