using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.DTO;
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
            var t = Db.ItensBaixaTipo1.FiltrarItensBaixaPagamento()
                .Select(i1 => new
                {
                    cnpj = i1.CnpjEmpresaCobranca,
                    dtBaixa = i1.DataBaixa
                })
                .Concat(Db.ItensBaixaTipo2.FiltrarItensBaixaPagamento()
                    .Select(i1 => new
                    {
                        cnpj = i1.CnpjEmpresaCobranca,
                        dtBaixa = i1.DataBaixa
                    }))
                .Concat(Db.ItensBaixaTipo3.FiltrarItensBaixaPagamento()
                    .Select(i1 => new
                    {
                        cnpj = i1.CnpjEmpresaCobranca,
                        dtBaixa = i1.DataBaixa
                    }))
                .OrderByDescending(i => i.dtBaixa)
                .GroupBy(i => new {i.cnpj, i.dtBaixa})
                .Select(i => new
                {
                    i.Key.cnpj,
                    items = TratarBusca(i.Key.dtBaixa, i.Key.cnpj)
                }).Paginar(0, 100);

            return t;
        }

        private IQueryable<object> TratarBusca(DateTime dtBaixa, string cnpj)
        {
            return Db.ItensBaixaTipo1.BuscarItems(dtBaixa, cnpj)
                .SelectItensBaixaPagamento(1)
                .Concat(Db.ItensBaixaTipo2.BuscarItems(dtBaixa, cnpj)
                    .SelectItensBaixaPagamento(2))
                .Concat(Db.ItensBaixaTipo3.BuscarItems(dtBaixa, cnpj)
                    .SelectItensBaixaPagamento(3));
        }
    }
}