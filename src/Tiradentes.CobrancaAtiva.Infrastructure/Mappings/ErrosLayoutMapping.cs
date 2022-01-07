using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class ErrosLayoutMapping : IEntityTypeConfiguration<ErrosLayoutModel>
    {
        public void Configure(EntityTypeBuilder<ErrosLayoutModel> builder)
        {
            builder.HasKey(ep => new { ep.DataHora, ep.Sequencia });

            builder.Property(ep => ep.DataHora)
                .HasColumnName("DAT_HORA");

            builder.Property(ep => ep.Sequencia)
              .HasColumnName("SEQ");

            builder.Property(ep => ep.Descricao)
              .HasColumnName("DSC_ERRO");
            

            builder.ToTable("ERROS_LAYOUT");
        }
    }
}