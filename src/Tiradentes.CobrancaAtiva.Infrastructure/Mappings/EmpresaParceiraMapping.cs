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

            builder.Property(ep => ep.Id)
               .HasColumnName("COD_EMPRESA");

            builder.Property(ep => ep.NomeFantasia)
                .HasColumnName("NOME_FANTASIA");

            builder.Property(ep => ep.RazaoSocial)
                .HasColumnName("RAZAO_SOCIAL");

            builder.Property(ep => ep.Sigla)
                .HasColumnName("SIGLA");

            builder.Property(ep => ep.CNPJ)
                .HasColumnName("CNPJ");

            builder.Property(ep => ep.NumeroContrato)
                .HasColumnName("NUMERO_CONTRATO");

            builder.Property(ep => ep.AditivoContrato)
                .HasColumnName("ADITIVO_CONTRATO");

            builder.Property(ep => ep.URL)
               .HasColumnName("URL_EMPRESA");

            builder.Property(ep => ep.Status)
               .HasColumnName("STATUS_EMPRESA");

            builder.HasMany(c => c.Contatos)
                .WithOne(e => e.Empresa);

            builder.HasOne(c => c.Endereco)
                .WithOne(e => e.Empresa);

            builder.ToTable("EMPRESA");
        }
    }
}
