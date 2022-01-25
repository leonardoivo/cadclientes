using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class HonorarioFaixaEmpresaParceiraMapping : IEntityTypeConfiguration<HonorarioFaixaEmpresaParceiraModel>
    {
        public void Configure(EntityTypeBuilder<HonorarioFaixaEmpresaParceiraModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_HONORARIO_FAIXA");

            builder.Property(ep => ep.PercentualJuros)
              .HasColumnName("PERCENTUAL_JUROS");

            builder.Property(ep => ep.PercentualMulta)
              .HasColumnName("PERCENTUAL_MULTA");

            builder.Property(ep => ep.PercentualRecebimentoAluno)
              .HasColumnName("PERCENTUAL_REC_ALUNO");

            builder.Property(ep => ep.VencidosAte)
              .HasColumnName("VENCIDOS_ATE");

            builder.Property(ep => ep.VencidosMaiorQue)
              .HasColumnName("VENCIDOS_MAIOR_QUE");                            

            builder.Property(ep => ep.HonorarioEmpresaParceiraId)
             .HasColumnName("COD_HONORARIO");

            builder.HasOne(c => c.HonorarioEmpresaParceira)
                .WithMany(e => e.Faixas)
                .HasForeignKey(c => c.HonorarioEmpresaParceiraId)
                .HasConstraintName("FK_FAIXA_HONORARIO");

            builder.ToTable("HONORARIO_FAIXA");
        }
    }
}
