using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class AcordosCobrancasMapping : IEntityTypeConfiguration<AcordosCobrancasModel>
    {
        public void Configure(EntityTypeBuilder<AcordosCobrancasModel> builder)
        {
            builder.HasKey(ep => new { ep.NumeroAcordo });

            builder.Property(ep => ep.NumeroAcordo)
                .HasColumnName("NUM_ACORDO");

            builder.Property(ep => ep.DataBaixa)
              .HasColumnName("DAT_BAIXA");

            builder.Property(ep => ep.Data)
              .HasColumnName("DATA");

            builder.Property(ep => ep.TotalParcelas)
               .HasColumnName("TOTAL_PARCELAS");

            builder.Property(ep => ep.ValorTotal)
               .HasColumnName("VALOR_TOTAL");

            builder.Property(ep => ep.Mora)
               .HasColumnName("MORA");
            

            builder.Property(ep => ep.Multa)
                .HasColumnName("MULTA");

            builder.Property(ep => ep.SaldoDevedor)
                .HasColumnName("SALDO_DEVEDOR");

            builder.Property(ep => ep.Matricula)
               .HasColumnName("MATRICLA");


            builder.Property(ep => ep.CnpjEmpresaCobranca)
               .HasColumnName("CNPJ_EMPRESA_COBRANCA");

            builder.Property(ep => ep.CPF)
               .HasColumnName("CPF")
               .HasColumnType("CHAR(11)");

            builder.Property(ep => ep.Sistema)
               .HasColumnName("SISTEMA")
               .HasColumnType("CHAR(1)");

            builder.Property(ep => ep.TipoInadimplencia)
               .HasColumnName("TIPO_INADIMPLENCIA")
               .HasColumnType("CHAR(1)");


            builder.ToTable("ACORDOS_COBRANCAS");
        }
    }
}
