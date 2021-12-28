using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class ItensGeracaoMapping : IEntityTypeConfiguration<ItensGeracaoModel>
    {
        public void Configure(EntityTypeBuilder<ItensGeracaoModel> builder)
        {
            builder.HasKey(ep => new {ep.DataGeracao, ep.CnpjEmpresaCobranca, ep.Matricula, ep.Parcela});

            builder.Ignore(ep => ep.Id);

            builder.Property(ep => ep.DataGeracao)
             .HasColumnName("DAT_GERACAO");

            builder.Property(ep => ep.CnpjEmpresaCobranca)
             .HasColumnName("CNPJ_EMPRESA_COBRANCA");

            builder.Property(ep => ep.Controle)
             .HasColumnName("CONTROLE");

            builder.Property(ep => ep.Parcela)
             .HasColumnName("PARCELA");

            builder.Property(ep => ep.Valor)
             .HasColumnName("VALOR");

            builder.Property(ep => ep.DataVencimento)
             .HasColumnName("DAT_VENC");

            builder.Property(ep => ep.Periodo)
             .HasColumnName("PERIODO");

            builder.Property(ep => ep.Matricula)
             .HasColumnName("MATRICULA");

            builder.Property(ep => ep.SituacaoAluno)
             .HasColumnName("STA_ALU");

            builder.Property(ep => ep.Sistema)
             .HasColumnName("SISTEMA");

            builder.Property(ep => ep.TipoInadimplencia)
             .HasColumnName("TIPO_INADIMPLENCIA");

            builder.Property(ep => ep.DescricaoInadimplencia)
             .HasColumnName("DSC_INADIMPLENCIA");

            builder.Property(ep => ep.PeriodoChequeDevolvido)
             .HasColumnName("PERIODO_CHEQUE_DEVOLVIDO");

            builder.ToTable("ITENS_GERACAO");
        }
    }
}
