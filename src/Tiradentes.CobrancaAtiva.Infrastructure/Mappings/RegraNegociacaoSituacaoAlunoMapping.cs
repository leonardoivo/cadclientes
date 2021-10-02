using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class RegraNegociacaoSituacaoAlunoMapping : IEntityTypeConfiguration<RegraNegociacaoSituacaoAlunoModel>
    {
        public void Configure(EntityTypeBuilder<RegraNegociacaoSituacaoAlunoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_REGRA_NEGOCIACAO_SIT_ALUNO");

            builder.Property(ep => ep.SituacaoAlunoId)
              .HasColumnName("COD_SITUACAO_ALUNO");

            builder.Property(ep => ep.RegraNegociacaoId)
              .HasColumnName("COD_REGRA_NEGOCIACAO");

            builder.HasOne(c => c.SituacaoAluno)
                .WithMany(e => e.RegraNegociacaoSituacaoAluno)
                .HasForeignKey(c => c.SituacaoAlunoId);

            builder.HasOne(c => c.RegraNegociacao)
                .WithMany(e => e.RegraNegociacaoSituacaoAluno)
                .HasForeignKey(c => c.RegraNegociacaoId);

            builder.ToTable("REGRA_NEGOCIACAO_SIT_ALUNO");
        }
    }
}
