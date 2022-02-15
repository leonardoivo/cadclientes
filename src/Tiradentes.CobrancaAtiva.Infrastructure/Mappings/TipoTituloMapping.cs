using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class TipoTituloMapping : IEntityTypeConfiguration<TipoTituloModel>
    {
        public void Configure(EntityTypeBuilder<TipoTituloModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_TIPO_TITULO");

            builder.Property(ep => ep.TipoTitulo)
             .HasColumnName("DESCRICAO_TITULO");

            builder.Property(ep => ep.CodigoMagister)
             .HasColumnName("COD_MAGISTER");

            builder.HasMany(c => c.RegraNegociacaoTipoTitulo)
               .WithOne(c => c.TipoTitulo);

            builder.ToTable("TIPO_TITULOS");
        }
    }
}
