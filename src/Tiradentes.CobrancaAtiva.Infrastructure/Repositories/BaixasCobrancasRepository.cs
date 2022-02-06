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
                                          && B.DataBaixa.Day == dataBaixa.Day).FirstOrDefaultAsync();
        }

        public void HabilitarAlteracaoBaixaCobranca(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_baix_cob({status.ToString().ToLower()});
                                         end;");
        }

        public async Task<object> Buscar()
        {
            var t = await Db.ItensBaixaTipo1.Where(i1 => i1.CnpjEmpresaCobranca != null).Select(i1 => new
                {
                    dt = i1.DataBaixa,
                    tp = 1,
                })
                .Concat(Db.ItensBaixaTipo2.Where(i2 => i2.CnpjEmpresaCobranca != null).Select(i2 => new
                {
                    dt = i2.DataBaixa,
                    tp = 2,
                }))
                .Concat(Db.ItensBaixaTipo3.Where(i3 => i3.CnpjEmpresaCobranca != null).Select(i3 => new
                {
                    dt = i3.DataBaixa,
                    tp = 3,
                })).
                OrderBy(i => i.dt).Paginar(0, 0);

            return t;
        }
    }
}