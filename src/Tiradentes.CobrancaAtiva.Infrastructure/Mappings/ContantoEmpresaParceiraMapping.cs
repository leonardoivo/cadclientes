using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ContantoEmpresaParceiraMapping : IEntityTypeConfiguration<ContatoEmpresaParceiraModel>
    {
        public void Configure(EntityTypeBuilder<ContatoEmpresaParceiraModel> builder)
        {
            builder.HasKey(ep => ep.Id);
            
            builder.Property(ep => ep.Id)
              .HasColumnName("COD_CONTATO");

            builder.Property(ep => ep.Contato)
              .HasColumnName("CONTATO");

            builder.Property(ep => ep.Email)
              .HasColumnName("EMAIL");

            builder.Property(ep => ep.Telefone)
              .HasColumnName("TELEFONE");

            builder.Property(ep => ep.EmpresaId)
              .HasColumnName("COD_EMPRESA");

            builder.HasOne(c => c.Empresa)
                .WithMany(e => e.Contatos)
                .HasForeignKey(c => c.EmpresaId);

            builder.ToTable("EMPRESAS_CONTATOS", "APP_COBRANCA");
        }
    }
}
