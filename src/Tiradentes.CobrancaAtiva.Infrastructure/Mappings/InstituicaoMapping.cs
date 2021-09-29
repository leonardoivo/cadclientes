using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class InstituicaoMapping : IEntityTypeConfiguration<InstituicaoModel>
    {
        public void Configure(EntityTypeBuilder<InstituicaoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_INSTITUICAO");

            builder.Property(ep => ep.Instituicao)
             .HasColumnName("INSTITUICAO");

            builder.HasMany(i => i.InstituicoesModalidades)
                .WithOne(im => im.Instituicao);

            builder.ToTable("INSTITUICOES");
        }
    }
}
