using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class ArquivoCobrancasMapping : IEntityTypeConfiguration<ArquivoCobrancasModel>
    {
        public void Configure(EntityTypeBuilder<ArquivoCobrancasModel> builder)
        {
            builder.Ignore(ep => ep.Id);

            builder.HasKey(ep => new {ep.DataGeracao, ep.CnpjEmpresaCobranca, ep.Sequencia});

            builder.Property(ep => ep.DataGeracao)
             .HasColumnName("DAT_GERACAO");

            builder.Property(ep => ep.CnpjEmpresaCobranca)
             .HasColumnName("CNPJ_EMPRESA_COBRANCA");

            builder.Property(ep => ep.Arquivo)
             .HasColumnName("ARQUIVO");

            builder.Property(ep => ep.Sequencia)
             .HasColumnName("SEQ");

            builder.Property(ep => ep.NomeLote)
             .HasColumnName("NOME_LOTE");

            builder.ToTable("ARQUIVO_COBRANCAS", "SCF");
        }
    }
}
