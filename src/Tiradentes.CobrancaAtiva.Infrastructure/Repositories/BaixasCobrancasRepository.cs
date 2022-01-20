using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class BaixasCobrancasRepository : BaseRepository<BaixasCobrancasModel>, IBaixasCobrancasRepository
    {
        public BaixasCobrancasRepository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public async Task<BaixasCobrancasModel> BuscarPorDataBaixa(DateTime dataBaixa)
        {
            return await DbSet.Where(B => B.DataBaixa.Year == dataBaixa.Year
                                       && B.DataBaixa.Month == dataBaixa.Month
                                       && B.DataBaixa.Day == dataBaixa.Day
                                       && B.DataBaixa.Hour == dataBaixa.Hour
                                       && B.DataBaixa.Minute == dataBaixa.Minute
                                       && B.DataBaixa.Second == dataBaixa.Second).FirstOrDefaultAsync();
        }

        public void HabilitarAlteracaoBaixaCobranca(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_baix_cob({status.ToString().ToLower()});
                                         end;");
        }
    }
}
