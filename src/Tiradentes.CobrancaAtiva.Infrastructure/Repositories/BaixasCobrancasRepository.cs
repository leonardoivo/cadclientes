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
            var t = await Db.ItensBaixaTipo1.Where(i1 => i1.CnpjEmpresaCobranca != null)
                .Select(ConverterParaBuscaDto<ItensBaixaTipo1Model>(1))
                .Concat(Db.ItensBaixaTipo2.Where(i2 => i2.CnpjEmpresaCobranca != null)
                    .Select(ConverterParaBuscaDto<ItensBaixaTipo2Model>(2)))
                .Concat(Db.ItensBaixaTipo3.Where(i3 => i3.CnpjEmpresaCobranca != null)
                    .Select(ConverterParaBuscaDto<ItensBaixaTipo3Model>(3))).OrderByDescending(i => i.DataBaixa)
                .GroupBy(i => new {i.DataBaixa, i.CNPJ}).Select(i => new
                    BuscaBaixaPagamentoDto
                    {
                        DataBaixa = i.Key.DataBaixa,
                        CNPJ = i.Key.CNPJ,
                    }).Paginar(0, 100);

            return t;
        }

        private static Expression<Func<TModel, ItensBaixaDto>> ConverterParaBuscaDto<TModel>(int tipo)
            where TModel : BaseItensModel
        {
            return i => new ItensBaixaDto
            {
                DataBaixa = i.DataBaixa,
                CNPJ = i.CnpjEmpresaCobranca,
                Tipo = tipo
            };
        }
    }
}