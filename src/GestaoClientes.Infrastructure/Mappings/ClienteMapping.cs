

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestaoClientes.Domain.Models;

namespace GestaoClientes.Infrastructure.Mappings
{
    class ClienteMapping : IEntityTypeConfiguration<ClienteModel>
    {
        public void Configure(EntityTypeBuilder<ClienteModel> builder)
        {
            builder.HasKey(ep => ep.IdCliente);

            builder.Property(ep => ep.IdCliente)
              .HasColumnName("IdCliente");

            builder.Property(ep => ep.Porte)
             .HasColumnName("Porte");

            builder.Property(ep => ep.Nome)
             .HasColumnName("Nome");

            builder.ToTable("Clientes", "GestaoClientes");
        }
    }
}
