using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ConflitoDetalheMapping : IEntityTypeConfiguration<ConflitoDetalheModel>
    {
        public void Configure(EntityTypeBuilder<ConflitoDetalheModel> builder)
        {
            builder.Property(cd => cd.ConflitoId)
              .HasColumnName("COD_CONFLITO");

            builder.Property(cd => cd.ModalidadeId)
              .HasColumnName("COD_MODALIDADE");

            builder.Property(cd => cd.Parcela)
               .HasColumnName("PARCELA");

            builder.Property(cd => cd.Valor)
               .HasColumnName("VALOR");

            builder.Property(cd => cd.DataEnvio)
               .HasColumnName("DATA_ENVIO");

            builder.HasOne(cd => cd.Conflito)
                .WithMany(cd => cd.ConflitoDetalhes)
                .HasForeignKey(cd => cd.ConflitoId);

            builder.ToTable("CONFLITOS_DETALHES");
        }
    }
}
