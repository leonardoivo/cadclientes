using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class RegraNegociacaoSemestreMapping : IEntityTypeConfiguration<RegraNegociacaoSemestreModel>
    {
        public void Configure(EntityTypeBuilder<RegraNegociacaoSemestreModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_REGRA_NEGOCIACAO_SEMESTRE");

            builder.Property(ep => ep.SemestreId)
              .HasColumnName("COD_SEMESTRE");

            builder.Property(ep => ep.RegraNegociacaoId)
              .HasColumnName("COD_REGRA_NEGOCIACAO");

            builder.HasOne(c => c.Semestre)
                .WithMany(e => e.RegraNegociacaoSemestre)
                .HasForeignKey(c => c.SemestreId);

            builder.HasOne(c => c.RegraNegociacao)
                .WithMany(e => e.RegraNegociacaoSemestre)
                .HasForeignKey(c => c.RegraNegociacaoId);

            builder.ToTable("REGRA_NEGOCIACAO_SEMESTRE");
        }
    }
}
