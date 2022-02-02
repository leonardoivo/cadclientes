using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ItensBaixaTipo2Mapping : IEntityTypeConfiguration<ItensBaixaTipo2Model>
    {
        public void Configure(EntityTypeBuilder<ItensBaixaTipo2Model> builder)
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
               .HasColumnName("MATRICULA");

            builder.Property(ep => ep.Periodo)
                .HasColumnName("PERIODO");

            builder.Property(ep => ep.Parcela)
              .HasColumnName("PARCELA");

            builder.Property(ep => ep.DataVencimento)
               .HasColumnName("DAT_VENC");

            builder.Property(ep => ep.Valor)
               .HasColumnName("VALOR");

            builder.Property(ep => ep.CnpjEmpresaCobranca)
                .HasColumnName("CNPJ_EMPRESA_COBRANCA");

            builder.Property(ep => ep.PeriodoOutros)
                .HasColumnName("PERIODO_OUTROS");

            builder.Property(ep => ep.SituacaoAluno)
                .HasColumnName("STA_ALU")
                .HasColumnType("CHAR(1)");

            builder.Property(ep => ep.Sistema)
                .HasColumnName("SISTEMA")
                .HasColumnType("CHAR(1)");

            builder.Property(ep => ep.TipoInadimplencia)
                .HasColumnName("TIPO_INADIMPLENCIA")
                .HasColumnType("CHAR(1)");

            builder.ToTable("ITENS_BAIXAS_TIPO2", "SCF");
        }
    }
}
