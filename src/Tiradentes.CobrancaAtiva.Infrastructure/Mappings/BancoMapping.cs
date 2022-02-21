using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class BancoMapping : IEntityTypeConfiguration<BancoModel>
    {
        public void Configure(EntityTypeBuilder<BancoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_BANCO");

            builder.Property(ep => ep.Codigo)
             .HasColumnName("CODIGO_BANCO");

            builder.Property(ep => ep.Nome)
             .HasColumnName("NOME_BANCO");

            builder.ToTable("BANCOS");
        }
    }
}
