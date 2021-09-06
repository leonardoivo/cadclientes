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
            
            builder.Property(ep => ep.Id)
              .HasColumnName("cod_contato");

            builder.Property(ep => ep.Contato)
              .HasColumnName("contato");

            builder.Property(ep => ep.Email)
              .HasColumnName("email");

            builder.Property(ep => ep.Telefone)
              .HasColumnName("telefone");

            builder.Property(ep => ep.EmpresaId)
              .HasColumnName("cod_empresa");

            builder.HasOne(c => c.Empresa)
                .WithMany(e => e.Contatos)
                .HasForeignKey(c => c.EmpresaId);

            builder.ToTable("EMPRESA_CONTATO");
        }
    }
}
