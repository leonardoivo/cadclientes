using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ContaBancariaEmpresaParceiraMapping : IEntityTypeConfiguration<ContaBancariaEmpresaParceiraModel>
    {
        public void Configure(EntityTypeBuilder<ContaBancariaEmpresaParceiraModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
               .HasColumnName("COD_EMPRESAS_CONTAS_BANCARIAS");

            builder.Property(ep => ep.ContaCorrente)
               .HasColumnName("CONTA_CORRENTE");

            builder.Property(ep => ep.CodigoAgencia)
                .HasColumnName("CODIGO_AGENCIA");

            builder.Property(ep => ep.Convenio)
                .HasColumnName("CONVENIO");

            builder.Property(ep => ep.Pix)
                .HasColumnName("PIX");

            builder.Property(ep => ep.EmpresaId)
              .HasColumnName("COD_EMPRESA");

            builder.Property(ep => ep.BancoId)
              .HasColumnName("COD_BANCO");

            builder.HasOne(ep => ep.Empresa)
                .WithOne(e => e.ContaBancaria)
                .HasForeignKey<ContaBancariaEmpresaParceiraModel>(e => e.EmpresaId);

            builder.HasOne(ep => ep.Banco)
                .WithMany(e => e.ContaBancarias)
                .HasForeignKey(e => e.BancoId);

            builder.ToTable("EMPRESAS_CONTAS_BANCARIAS", "APP_COBRANCA");
        }
    }
}