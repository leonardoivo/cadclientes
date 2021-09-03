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

            builder.Property(ep => ep.CEP)
                .HasColumnName("cep");

            builder.Property(ep => ep.Estado)
                .HasColumnName("estado");

            builder.Property(ep => ep.Cidade)
                .HasColumnName("cidade");

            builder.Property(ep => ep.Logradouro)
                .HasColumnName("logradouro");

            builder.Property(ep => ep.Numero)
                .HasColumnName("numero");

            builder.Property(ep => ep.Complemento)
                .HasColumnName("complemento");

            builder.Property(ep => ep.Status)
               .HasColumnName("status");

            builder.ToTable("EMPRESA");
        }
    }
}
