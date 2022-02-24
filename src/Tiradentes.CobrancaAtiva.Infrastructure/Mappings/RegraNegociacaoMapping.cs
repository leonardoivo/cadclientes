using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class RegraNegociacaoMapping : IEntityTypeConfiguration<RegraNegociacaoModel>
    {
        public void Configure(EntityTypeBuilder<RegraNegociacaoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_REGRA_NEGOCIACAO");

            builder.Property(ep => ep.InstituicaoId)
              .HasColumnName("COD_INSTITUICAO");

            builder.Property(ep => ep.ModalidadeId)
              .HasColumnName("COD_MODALIDADE");

            builder.Property(ep => ep.PercentJurosMultaAVista)
              .HasColumnName("PERCENT_JUROS_MULTA_AVISTA")
              .HasColumnType("NUMBER(3,5)");

            builder.Property(ep => ep.PercentValorAVista)
              .HasColumnName("PERCENT_VALOR_AVISTA")
              .HasColumnType("NUMBER(3,5)");


            builder.Property(ep => ep.PercentJurosMultaCartao)
              .HasColumnName("PERCENT_JUROS_MULTA_CARTAO")
              .HasColumnType("NUMBER(3,5)");

            builder.Property(ep => ep.PercentValorCartao)
              .HasColumnName("PERCENT_VALOR_CARTAO")
              .HasColumnType("NUMBER(3,5)");

            builder.Property(ep => ep.QuantidadeParcelasCartao)
              .HasColumnName("QUANT_PARCELA_CARTAO");

            builder.Property(ep => ep.PercentJurosMultaBoleto)
              .HasColumnName("PERCENT_JUROS_MULTA_BOLETO")
              .HasColumnType("NUMBER(3,5)");

            builder.Property(ep => ep.PercentValorBoleto)
              .HasColumnName("PERCENT_VALOR_BOLETO")
              .HasColumnType("NUMBER(3,5)");

            builder.Property(ep => ep.PercentEntradaBoleto)
              .HasColumnName("PERCENT_ENTRADA_BOLETO")
              .HasColumnType("NUMBER(3,5)");

            builder.Property(ep => ep.QuantidadeParcelasBoleto)
              .HasColumnName("QUANT_PARCELA_BOLETO");

            builder.Property(ep => ep.Status)
              .HasColumnName("STATUS")
              .HasColumnType("NUMBER(1)");

            builder.Property(ep => ep.InadimplenciaInicial)
              .HasColumnName("INADIMPLENCIA_INICIAL")
              .HasColumnType("DATE");

            builder.Property(ep => ep.InadimplenciaFinal)
              .HasColumnName("INADIMPLENCIA_FINAL")
              .HasColumnType("DATE");

            builder.Property(ep => ep.ValidadeInicial)
              .HasColumnName("VALIDADE_INICIAL")
              .HasColumnType("DATE");

            builder.Property(ep => ep.ValidadeFinal)
              .HasColumnName("VALIDADE_FINAL")
              .HasColumnType("DATE");

            builder.HasOne(im => im.Instituicao)
                .WithMany(m => m.RegraNegociacao)
                .HasForeignKey(im => im.InstituicaoId);

            builder.HasOne(im => im.Modalidade)
                .WithMany(m => m.RegraNegociacao)
                .HasForeignKey(im => im.ModalidadeId);

            builder.HasMany(c => c.RegraNegociacaoCurso)
                       .WithOne(e => e.RegraNegociacao);

            builder.HasMany(c => c.RegraNegociacaoTituloAvulso)
              .WithOne(e => e.RegraNegociacao);

            builder.HasMany(c => c.RegraNegociacaoSituacaoAluno)
              .WithOne(e => e.RegraNegociacao);

            builder.HasMany(c => c.RegraNegociacaoTipoTitulo)
              .WithOne(e => e.RegraNegociacao);

            builder.ToTable("REGRA_NEGOCIACAO", "APP_COBRANCA");
        }
    }
}

