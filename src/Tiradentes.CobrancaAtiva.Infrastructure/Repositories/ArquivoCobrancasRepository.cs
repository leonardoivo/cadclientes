using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ArquivoCobrancasRepository : IArquivoCobrancasRepository
    {
        private readonly CobrancaAtivaScfDbContext _context;
        public ArquivoCobrancasRepository(CobrancaAtivaScfDbContext context) 
        { 
            _context = context;
        }

        public virtual async Task Criar(ArquivoCobrancasModel model)
        {
            await _context.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_arq_cob( true ); END;");

            var pArquivo = new OracleParameter("ARQUIVO", OracleDbType.Clob, model.Arquivo, ParameterDirection.Input);

            await _context.Database.ExecuteSqlRawAsync("BEGIN " +
                "insert into scf.arquivo_cobrancas  " +
                "(DAT_GERACAO, SEQ, ARQUIVO, CNPJ_EMPRESA_COBRANCA, NOME_LOTE)  " +
                "values ( " +
                "to_date('" + model.DataGeracao + "','dd/mm/yyyy hh24:mi:ss'), " + 
                "" + model.Sequencia + ", " +
                "{0}, " + 
                "" + model.CnpjEmpresaCobranca + ", " +
                "'" + model.NomeLote + "'); " +
                "END;", pArquivo);
                
            await SaveChanges();
            await _context.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_arq_cob( false ); END;");
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
