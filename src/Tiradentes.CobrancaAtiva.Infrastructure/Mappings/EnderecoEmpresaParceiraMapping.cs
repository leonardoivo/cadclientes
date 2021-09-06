using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class EnderecoEmpresaParceiraMapping : IEntityTypeConfiguration<EnderecoEmpresaParceiraModel>
    {
        public void Configure(EntityTypeBuilder<EnderecoEmpresaParceiraModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
               .HasColumnName("COD_ENDERECO");

            builder.Property(ep => ep.CEP)
               .HasColumnName("CEP");

            builder.Property(ep => ep.Estado)
                .HasColumnName("ESTADO");

            builder.Property(ep => ep.Cidade)
                .HasColumnName("CIDADE");

            builder.Property(ep => ep.Logradouro)
                .HasColumnName("LOGRADOURO");

            builder.Property(ep => ep.Numero)
                .HasColumnName("NUMERO");

            builder.Property(ep => ep.Complemento)
                .HasColumnName("COMPLEMENTO");

            builder.Property(ep => ep.EmpresaId)
              .HasColumnName("COD_EMPRESA");

            builder.HasOne(ep => ep.Empresa)
                .WithOne(e => e.Endereco)
                .HasForeignKey<EnderecoEmpresaParceiraModel>(e => e.EmpresaId);

            builder.ToTable("EMPRESA_ENDERECO");
        }
    }
}