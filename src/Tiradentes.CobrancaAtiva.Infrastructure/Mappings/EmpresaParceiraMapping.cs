using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class EmpresaParceiraMapping : IEntityTypeConfiguration<EmpresaParceiraModel>
    {
    public void Configure(EntityTypeBuilder<EmpresaParceiraModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.NomeFantasia)
                .IsRequired()
                .HasColumnName("nome_fantasia");

            builder.Property(ep => ep.CNPJ)
                .IsRequired()
                .HasColumnName("cnpj");

            builder.ToTable("empresa_parceira");
        }
    }
}
