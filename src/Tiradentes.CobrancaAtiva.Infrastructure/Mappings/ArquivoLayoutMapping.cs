using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ArquivoLayoutMapping : IEntityTypeConfiguration<ArquivoLayoutModel>
    {
        public void Configure(EntityTypeBuilder<ArquivoLayoutModel> builder)
        {
            builder.Ignore(ep => ep.Id);

            builder.ToTable("ARQUIVO_LAYOUT", "SCF");


            builder.HasKey(ep => ep.DataHora);

            builder.Property(ep => ep.DataHora)
                .HasColumnName("DAT_HORA");

            builder.Property(ep => ep.NomeUsuario)
              .HasColumnName("USERNAME");

            builder.Property(ep => ep.Status)
              .HasColumnName("STATUS");

            builder.Property(ep => ep.Conteudo)
               .HasColumnName("CONTEUDO");

            //builder.HasMany(b => b.ErrosLayout).WithOne();

        }
    }
}
