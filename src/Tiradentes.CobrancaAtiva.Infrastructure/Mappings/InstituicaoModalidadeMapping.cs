using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class InstituicaoModalidadeMapping : IEntityTypeConfiguration<InstituicaoModalidadeModel>
    {
        public void Configure(EntityTypeBuilder<InstituicaoModalidadeModel> builder)
        {
            builder.HasKey(e => new { e.InstituicaoId, e.ModalidadeId });

            builder.Property(ep => ep.InstituicaoId)
             .HasColumnName("COD_INSTITUICAO");

            builder.Property(ep => ep.ModalidadeId)
             .HasColumnName("COD_MODALIDADE");

            builder.HasOne(im => im.Instituicao)
                .WithMany(i => i.InstituicoesModalidades)
                .HasForeignKey(im => im.InstituicaoId);

            builder.HasOne(im => im.Modalidade)
                .WithMany(m => m.InstituicoesModalidades)
                .HasForeignKey(im => im.ModalidadeId);

            builder.ToTable("INSTITUICAO_MODALIDADE");
        }
    }
}
