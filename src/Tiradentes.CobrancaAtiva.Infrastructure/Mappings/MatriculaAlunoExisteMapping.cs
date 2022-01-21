
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class MatriculaAlunoExisteMapping : IEntityTypeConfiguration<MatriculaAlunoExisteModel>
    {
        public void Configure(EntityTypeBuilder<MatriculaAlunoExisteModel> builder)
        {
            builder.HasNoKey();

            builder.Property(P => P.Count)
                   .HasColumnName("ALUNOCOUNT");
        }
    }
}
