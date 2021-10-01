using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class CursoMapping : IEntityTypeConfiguration<CursoModel>
    {
        public void Configure(EntityTypeBuilder<CursoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_CURSO");

            builder.Property(ep => ep.Descricao)
             .HasColumnName("DESCRICAO_CURSO");

            builder.HasMany(c => c.RegraNegociacaoCurso)
               .WithOne(e => e.Curso);

            builder.ToTable("CURSOS");
        }
    }
}
