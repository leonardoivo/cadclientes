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

            builder.Property(ep => ep.TipoEnvioArquivo)
               .HasColumnName("TIPO_ENVIO_ARQUIVO")
               .HasDefaultValue("");

            builder.Property(ep => ep.IpEnvioArquivo)
               .HasColumnName("IP_ENVIO_ARQUIVO")
               .HasDefaultValue("");

            builder.Property(ep => ep.PortaEnvioArquivo)
               .HasColumnName("PORTA_ENVIO_ARQUIVO")
               .HasDefaultValue(22);

            builder.Property(ep => ep.UsuarioEnvioArquivo)
               .HasColumnName("USUARIO_ENVIO_ARQUIVO")
               .HasDefaultValue("");

            builder.Property(ep => ep.SenhaEnvioArquivo)
               .HasColumnName("SENHA_ENVIO_ARQUIVO")
               .HasDefaultValue("");

            builder.HasMany(c => c.Contatos)
                .WithOne(e => e.Empresa);

            builder.HasOne(c => c.Endereco)
                .WithOne(e => e.Empresa);

            builder.HasOne(c => c.ContaBancaria)
                .WithOne(e => e.Empresa);

            builder.Property(ep => ep.ChaveIntegracaoSap)
                .HasColumnName("CHAVE_INTEGRACAO_SAP")
                .HasDefaultValue("");

            builder.ToTable("EMPRESAS", "APP_COBRANCA");
        }
    }
}
