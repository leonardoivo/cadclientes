using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class EnderecoEmpresaParceiraMapping : IEntityTypeConfiguration<EnderecoEmpresaParceiraModel>
    {
        public void Configure(EntityTypeBuilder<EnderecoEmpresaParceiraModel> builder)
        {
            builder.HasKey(ep => ep.Id).HasName("cod_empresa");

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

            builder.Property(ep => ep.EmpresaId)
              .HasColumnName("cod_empresa");

            builder.HasOne(c => c.Empresa)
                .WithOne(e => e.Endereco)
                .HasForeignKey("cod_empresa");

            builder.ToTable("EMPRESA_ENDERECO");
        }
    }
}