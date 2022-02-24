using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class HonorarioEmpresaParceiraMapping : IEntityTypeConfiguration<HonorarioEmpresaParceiraModel>
    {
        public void Configure(EntityTypeBuilder<HonorarioEmpresaParceiraModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_HONORARIO");

            builder.Property(ep => ep.PercentualCobrancaIndevida)
              .HasColumnName("PERC_COBRANCA_INDEVIDA");

            builder.Property(ep => ep.ValorCobrancaIndevida)
              .HasColumnName("VALOR_COBRANCA_INDEVIDA");

            builder.Property(ep => ep.FaixaEspecialPercentualJuros)
              .HasColumnName("FX_ESPECIAL_PERCENTUAL_JUROS");

            builder.Property(ep => ep.FaixaEspecialPercentualMulta)
              .HasColumnName("FX_ESPECIAL_PERCENTUAL_MULTA");

            builder.Property(ep => ep.FaixaEspecialPercentualRecebimentoAluno)
              .HasColumnName("FX_ESPECIAL_PERCENTUAL_REC_ALUNO");

            builder.Property(ep => ep.FaixaEspecialVencidosAte)
              .HasColumnName("FX_ESPECIAL_VENCIDOS_ATE");

            builder.Property(ep => ep.FaixaEspecialVencidosMaiorQue)
              .HasColumnName("FX_ESPECIAL_VENCIDOS_MAIOR_QUE");                            

            builder.Property(ep => ep.EmpresaParceiraId)
             .HasColumnName("COD_EMPRESA");

            builder.HasOne(c => c.EmpresaParceira)
                .WithMany(e => e.Honorarios)
                .HasForeignKey(c => c.EmpresaParceiraId)
                .HasConstraintName("FK_HONORARIO_COD_EMPRESA");

            builder.ToTable("HONORARIOS", "APP_COBRANCA");
        }
    }
}
