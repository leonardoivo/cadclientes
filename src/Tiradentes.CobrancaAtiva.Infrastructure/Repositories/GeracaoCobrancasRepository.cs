using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class GeracaoCobrancasRepository : IGeracaoCobrancasRepository
    {
        private readonly CobrancaAtivaScfDbContext _context;
        public GeracaoCobrancasRepository(CobrancaAtivaScfDbContext context) 
        { 
            _context = context;
        }

        public virtual async Task Criar(GeracaoCobrancasModel model)
        {
            await _context.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_ger_cob( true ); END;");
            //_context.GeracaoCobrancas.Add(model);
            await _context.Database.ExecuteSqlRawAsync("BEGIN " +
            "insert into scf.geracao_cobrancas  " +
            "    (dat_geracao, dat_ini, dat_fim, username, sistema, dat_hora_envio, cnpj_empresa_cobranca,tipo_inadimplencia)  " +
            "    values  " +
            "    ( " +
            "    sysdate,  " +
            "    to_date('" + model.DataInicio + " 11:11:11','dd/mm/yyyy hh24:mi:ss'),  " +
            "    to_date('" + model.DataFim + " 11:11:11','dd/mm/yyyy hh24:mi:ss')-1,  " +
            "    'APP_COBRANCA', " +
            "    '" + model.Sistema + "',  " +
            "    to_date('" + model.DataHoraEnvio + " 10:10:10','dd/mm/yyyy hh24:mi:ss'), " +
            "    " + model.CnpjEmpresaCobranca + ", " +
            "    '" + model.TipoInadimplencia + "'); END;");

            await SaveChanges();
            await _context.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_ger_cob( false ); END;");
            await SaveChanges();

            var geracaoCriada = await _context.GeracaoCobrancas.FromSqlRaw("select to_char(t.dat_geracao,'dd/mm/yyyy hh24:mi:ss') DAT_GERACAO, " + 
            "t.dat_ini, t.dat_fim, t.username, t.sistema, t.dat_hora_envio, t.cnpj_empresa_cobranca, t.tipo_inadimplencia " +
            "from scf.geracao_cobrancas t " + 
            "order by t.dat_geracao desc").FirstAsync();

            model.DataGeracao = geracaoCriada.DataGeracao;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
