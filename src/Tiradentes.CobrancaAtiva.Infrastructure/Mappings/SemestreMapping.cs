using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class SemestreMapping : IEntityTypeConfiguration<SemestreModel>
    {
        public void Configure(EntityTypeBuilder<SemestreModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_SEMESTRE");

            builder.Property(ep => ep.Descricao)
             .HasColumnName("DESCRICAO_SEMESTRE");

            builder.Property(ep => ep.Numeral)
             .HasColumnName("NUMERAL_SEMESTRE");

            builder.ToTable("SEMESTRES", "APP_COBRANCA");
        }
    }
}
