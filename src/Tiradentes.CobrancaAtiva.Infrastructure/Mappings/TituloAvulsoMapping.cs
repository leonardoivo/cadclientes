using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class TituloAvulsoMapping : IEntityTypeConfiguration<TituloAvulsoModel>
    {
        public void Configure(EntityTypeBuilder<TituloAvulsoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_TIT_AVULSO");

            builder.Property(ep => ep.Descricao)
             .HasColumnName("DESCRICAO");

            builder.Property(ep => ep.CodigoGT)
             .HasColumnName("COD_GT");

            builder.ToTable("TITULO_AVULSO");
        }
    }
}
