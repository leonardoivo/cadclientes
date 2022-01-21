using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ParcelaPagaAlunoInstituicaoMapping : IEntityTypeConfiguration<ParcelaPagaAlunoInstituicaoModel>
    {
        public void Configure(EntityTypeBuilder<ParcelaPagaAlunoInstituicaoModel> builder)
        {
            builder.HasNoKey();

            builder.Property(P => P.Count)
                .HasColumnName("PARCELAPAGACOUNT");
        }
    }
}
