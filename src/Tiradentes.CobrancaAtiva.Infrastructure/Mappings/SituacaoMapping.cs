using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class SituacaoMapping : IEntityTypeConfiguration<SituacaoModel>
    {
        public void Configure(EntityTypeBuilder<SituacaoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_TIPO_SITUACAO");

            builder.Property(ep => ep.Situacao)
             .HasColumnName("DESCRICAO_SITUACAO");

            builder.ToTable("SITUACOES");
        }
    }
}
