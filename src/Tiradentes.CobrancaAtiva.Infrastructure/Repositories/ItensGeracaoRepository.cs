using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ItensGeracaoRepository : IItensGeracaoRepository
    {
        private readonly CobrancaAtivaScfDbContext _context;
        public ItensGeracaoRepository(CobrancaAtivaScfDbContext context) 
        { 
            _context = context;
        }

        public virtual async Task Criar(ItensGeracaoModel model)
        {
            await _context.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_itens_ger( true ); END;");
            //_context.ItensGeracao.Add(model);

            await _context.Database.ExecuteSqlRawAsync("BEGIN " +
                "insert into scf.itens_geracao  " +
                "(dat_geracao, matricula, periodo, parcela, dat_venc, valor, controle, cnpj_empresa_cobranca,sta_alu,sistema,tipo_inadimplencia,dsc_inadimplencia)  " +
                "values ( " +
                "to_date('" + model.DataGeracao + "','dd/mm/yyyy hh24:mi:ss'), " + 
                "" + model.Matricula + ", " +
                "" + model.Periodo + ", " + 
                "" + model.Parcela + ", " +
                "'" + model.DataVencimento + "', " + 
                "" + model.Valor.ToString().Replace(",", ".") + ", " + 
                "'" + model.Controle + "', " +
                "" + model.CnpjEmpresaCobranca + ", " +
                "'" + model.SituacaoAluno + "', " +
                "'" + model.Sistema + "', " +
                "'" + model.TipoInadimplencia + "', " +
                "'" + model.DescricaoInadimplencia + "'); " +
                "END;");

            await SaveChanges();
            await _context.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_itens_ger( false ); END;");
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
