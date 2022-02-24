using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    public class ParametroEnvioSituacaoAlunoMapping : IEntityTypeConfiguration<ParametroEnvioSituacaoAlunoModel>
    {
        public void Configure(EntityTypeBuilder<ParametroEnvioSituacaoAlunoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
              .HasColumnName("COD_PARAMETRO_ENVIO_SIT_ALUNO");

            builder.Property(ep => ep.SituacaoAlunoId)
              .HasColumnName("COD_SITUACAO_ALUNO");

            builder.Property(ep => ep.ParametroEnvioId)
              .HasColumnName("COD_PARAMETRO_ENVIO");

            builder.HasOne(c => c.SituacaoAluno)
                .WithMany(e => e.ParametroEnvioSituacaoAluno)
                .HasForeignKey(c => c.SituacaoAlunoId);

            builder.HasOne(c => c.ParametroEnvio)
                .WithMany(e => e.ParametroEnvioSituacaoAluno)
                .HasForeignKey(c => c.ParametroEnvioId);

            builder.ToTable("PARAMETRO_ENVIO_SIT_ALUNO", "APP_COBRANCA");
        }
    }
}
