using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ContantoEmpresaParceiraMapping : IEntityTypeConfiguration<ContatoEmpresaParceiraModel>
    {
        public void Configure(EntityTypeBuilder<ContatoEmpresaParceiraModel> builder)
        {
            builder.HasKey(ep => ep.Id).HasName("cod_contato");

            builder.Property(ep => ep.Contato)
              .HasColumnName("contato");

            builder.Property(ep => ep.Email)
              .HasColumnName("email");

            builder.Property(ep => ep.Telefone)
              .HasColumnName("telefone");

            builder.ToTable("EMPRESA_CONTATO");
        }
    }
}
