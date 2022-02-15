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

            builder.Property(ep => ep.InstituicaoId)
             .HasColumnName("COD_INSTITUICAO");

            builder.Property(ep => ep.ModalidadeId)
             .HasColumnName("COD_MODALIDADE");

            builder.Property(ep => ep.CodigoMagister)
             .HasColumnName("COD_MAGISTER");

            builder.HasOne(im => im.Instituicao)
                .WithMany(m => m.Cursos)
                .HasForeignKey(im => im.InstituicaoId);

            builder.HasOne(im => im.Modalidade)
                .WithMany(m => m.Cursos)
                .HasForeignKey(im => im.ModalidadeId);

            builder.HasMany(c => c.RegraNegociacaoCurso)
               .WithOne(r => r.Curso);

            builder.ToTable("CURSOS");
        }
    }
}
