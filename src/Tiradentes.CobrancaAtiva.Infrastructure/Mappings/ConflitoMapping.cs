using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class ConflitoMapping : IEntityTypeConfiguration<ConflitoModel>
    {
        public void Configure(EntityTypeBuilder<ConflitoModel> builder)
        {
            builder.HasKey(ep => ep.Id);

            builder.Property(ep => ep.Id)
                .HasColumnName("COD_CONFLITO");

            builder.Property(ep => ep.NomeLote)
              .HasColumnName("NOME_LOTE");
            
            builder.Property(ep => ep.EmpresaParceiraTentativaId)
              .HasColumnName("COD_EMPRESA_PARCEIRA_TENTATIVA");

            builder.Property(ep => ep.EmpresaParceiraEnvioId)
              .HasColumnName("COD_EMPRESA_PARCEIRA_ENVIO");

            builder.Property(ep => ep.NomeAluno)
              .HasColumnName("NOME_ALUNO");

            builder.Property(ep => ep.Matricula)
              .HasColumnName("MATRICULA");

            builder.Property(ep => ep.CPF)
              .HasColumnName("CPF");

            builder.Property(ep => ep.ModalidadeId)
               .HasColumnName("COD_MODALIDADE");

            builder.Property(ep => ep.Parcela)
               .HasColumnName("PARCELA");

            // builder.Property(ep => ep.SituacaoConflito)
            //.HasColumnName("SituacaoConflito");

            builder.Property(ep => ep.Valor)
               .HasColumnName("VALOR");

            builder.Property(ep => ep.DataEnvio)
               .HasColumnName("DATA_ENVIO");

            builder.ToTable("CONFLITOS");
        }
    }
}
