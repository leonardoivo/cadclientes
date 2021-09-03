using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class EmpresaParceiraMapping : IEntityTypeConfiguration<EmpresaParceiraModel>
    {
        public void Configure(EntityTypeBuilder<EmpresaParceiraModel> builder)
        {
            builder.HasKey(ep => ep.Id).HasName("cod_empresa");

            builder.Property(ep => ep.NomeFantasia)
                .HasColumnName("nome_fantasia");

            builder.Property(ep => ep.RazaoSocial)
                .HasColumnName("razao_social");

            builder.Property(ep => ep.Sigla)
                .HasColumnName("sigla");

            builder.Property(ep => ep.CNPJ)
                .HasColumnName("cnpj");

            builder.Property(ep => ep.NumeroContrato)
                .HasColumnName("numero_contrato");

            //builder.Property(ep => ep.AditivoContrato)
            //    .HasColumnName("aditivo_contrato");

            builder.Property(ep => ep.URL)
               .HasColumnName("url_empresa");

            builder.Property(ep => ep.Status)
               .HasColumnName("status");

            builder.HasMany(c => c.Contatos)
                .WithOne(e => e.Empresa);

            builder.HasOne(c => c.Endereco)
                .WithOne(e => e.Empresa);

            builder.ToTable("EMPRESA");
        }
    }
}
