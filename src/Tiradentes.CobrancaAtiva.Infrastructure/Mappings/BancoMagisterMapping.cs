using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class BancoMagisterMapping : IEntityTypeConfiguration<BancoMagisterModel>
    {
        public void Configure(EntityTypeBuilder<BancoMagisterModel> builder)
        {
            builder.Ignore(b => b.Id);
            builder.HasKey(b => b.Codigo);

            builder.Property(ep => ep.Codigo)
              .HasColumnName("COD_BANCO");

            builder.Property(ep => ep.Nome)
             .HasColumnName("NOM_BANCO");

            builder.Property(ep => ep.Digito)
             .HasColumnName("DIGITO");

            builder.Property(ep => ep.ContaContabil)
             .HasColumnName("CONTA_CONTABIL");

            builder.ToTable("BANCOS", "SCF");
        }
    }
}
