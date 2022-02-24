using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class RegraNegociacaoCursoMapping : IEntityTypeConfiguration<RegraNegociacaoCursoModel>
    {
        public void Configure(EntityTypeBuilder<RegraNegociacaoCursoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_REGRA_NEGOCIACAO_CURSO");

            builder.Property(ep => ep.CursoId)
              .HasColumnName("COD_CURSO");

            builder.Property(ep => ep.RegraNegociacaoId)
              .HasColumnName("COD_REGRA_NEGOCIACAO");

            builder.HasOne(c => c.Curso)
                .WithMany(e => e.RegraNegociacaoCurso)
                .HasForeignKey(c => c.CursoId);

            builder.HasOne(c => c.RegraNegociacao)
                .WithMany(e => e.RegraNegociacaoCurso)
                .HasForeignKey(c => c.RegraNegociacaoId);

            builder.ToTable("REGRA_NEGOCIACAO_CURSO", "APP_COBRANCA");
        }
    }
}
