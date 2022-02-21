using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ItensGeracaoRepository : BaseRepository<ItensGeracaoModel>, IItensGeracaoRepository
    {
        public ItensGeracaoRepository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public bool ExisteMatricula(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela, string periodoOutros)
        {
            return DbSet.Where(I => I.CnpjEmpresaCobranca == cnpjEmpresa
                                 && I.Matricula == matricula
                                 && I.Periodo == periodo
                                 && I.PeriodoOutros == periodoOutros
                                 && I.Parcela == parcela).Count() > 0;
        }

        public DateTime ObterDataEnvio(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela, string periodoOutros)
        {
            return DbSet.Where(I => I.CnpjEmpresaCobranca == cnpjEmpresa
                                 && I.Matricula == matricula
                                 && I.Periodo == periodo
                                 && I.PeriodoOutros == periodoOutros
                                 && I.Parcela == parcela)
                        .Select(I => I.DataGeracao).FirstOrDefault();
        }


        public virtual async Task Criar(ItensGeracaoModel model)
        {
            await Db.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_itens_ger( true ); END;");

            await Db.Database.ExecuteSqlRawAsync("BEGIN " +
                "insert into scf.itens_geracao  " +
                "(dat_geracao, matricula, periodo, parcela, dat_venc, valor, controle, cnpj_empresa_cobranca,sta_alu,sistema,tipo_inadimplencia,dsc_inadimplencia, periodo_outros)  " +
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
                "'" + model.DescricaoInadimplencia + "', " +
                "'" + model.PeriodoOutros + "'); " +
                "END;");

            await SaveChanges();
            await Db.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_itens_ger( false ); END;");
            await SaveChanges();
        }

        public virtual async Task CriarVarios(IList<ItensGeracaoModel> models)
        {
            await Db.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_itens_ger( true ); END;");

            var sqlCommand = "BEGIN INSERT ALL ";

            foreach (var model in models)
            {
                sqlCommand += " into scf.itens_geracao  " +
                "(dat_geracao, matricula, periodo, parcela, dat_venc, valor, controle, cnpj_empresa_cobranca,sta_alu,sistema,tipo_inadimplencia,dsc_inadimplencia,periodo_outros)  " +
                "values ( " +
                "to_date('" + model.DataGeracao + "','dd/mm/yyyy hh24:mi:ss'), " +
                "" + model.Matricula + ", " +
                "" + model.Periodo + ", " +
                "" + model.Parcela + ", " +
                "'" + model.DataVencimento.ToString("d") + "', " +
                "" + model.Valor.ToString().Replace(",", ".") + ", " +
                "'" + model.Controle + "', " +
                "" + model.CnpjEmpresaCobranca + ", " +
                "'" + model.SituacaoAluno + "', " +
                "'" + model.Sistema + "', " +
                "'" + model.TipoInadimplencia + "', " +
                "'" + model.DescricaoInadimplencia + "', " +
                (model.PeriodoOutros == null || model.PeriodoOutros.Equals("") ? "null" : "'" + model.PeriodoOutros + "'") + ") ";
            }

            sqlCommand += " SELECT 1 FROM DUAL; END;";

            await Db.Database.ExecuteSqlRawAsync(sqlCommand);

            await SaveChanges();
            await Db.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_itens_ger( false ); END;");
            await SaveChanges();
        }
    }
}

