using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Mappings
{
    class GeracaoCobrancasMapping : IEntityTypeConfiguration<GeracaoCobrancasModel>
    {
        public void Configure(EntityTypeBuilder<GeracaoCobrancasModel> builder)
        {
            builder.HasKey(ep => new {ep.DataGeracao, ep.CnpjEmpresaCobranca});

            builder.Ignore(ep => ep.Id);

            builder.Property(ep => ep.DataGeracao)
             .HasColumnName("DAT_GERACAO");

            builder.Property(ep => ep.DataInicio)
             .HasColumnName("DAT_INI");

            builder.Property(ep => ep.DataFim)
             .HasColumnName("DAT_FIM");

            builder.Property(ep => ep.Username)
             .HasColumnName("USERNAME");

            builder.Property(ep => ep.DataHoraEnvio)
             .HasColumnName("DAT_HORA_ENVIO");

            builder.Property(ep => ep.Sistema)
             .HasColumnName("SISTEMA");

            builder.Property(ep => ep.CnpjEmpresaCobranca)
             .HasColumnName("CNPJ_EMPRESA_COBRANCA");

            builder.Property(ep => ep.TipoInadimplencia)
             .HasColumnName("TIPO_INADIMPLENCIA");

            builder.ToTable("GERACAO_COBRANCAS");
        }
    }
}
