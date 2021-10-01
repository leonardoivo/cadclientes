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
              .HasColumnName("COD_INDTITUICAO");

            builder.Property(ep => ep.ModedalidadeId)
              .HasColumnName("COD_REGRA_NEGOCIACAO");

            builder.Property(ep => ep.PercentJurosMulta)
              .HasColumnName("PERCENT_JUROS_MULTA");

            builder.Property(ep => ep.PercentValor)
              .HasColumnName("PERCENT_VALOR");

            builder.HasMany(c => c.RegraNegociacaoCurso)
               .WithOne(e => e.RegraNegociacao);

            builder.HasMany(c => c.RegraNegociacaoSemestre)
              .WithOne(e => e.RegraNegociacao);

            builder.HasMany(c => c.RegraNegociacaoSituacaoAluno)
              .WithOne(e => e.RegraNegociacao);

            builder.HasMany(c => c.RegraNegocicaoTipoPagamento)
              .WithOne(e => e.RegraNegociacao);

            builder.HasMany(c => c.RegraNegociacaoTipoTitulo)
              .WithOne(e => e.RegraNegociacao);

            builder.ToTable("REGRA_NEGOCIACAO");
        }
    }
}

