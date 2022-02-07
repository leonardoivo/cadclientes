using System;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories.Helpers
{
    public static class BuscaItensBaixaCobranca
    {
        public static IQueryable<TModel> FiltrarItensBaixaPagamento<TModel>(
            this IQueryable<TModel> query, BaixaCobrancaQueryParam queryParam) where TModel : BaseItensModel
        {
            query = query.Where(i => i.CnpjEmpresaCobranca != null);
            //Alterar depois de começar a salvar a ies
            if (queryParam.EmpresaParceiraId.HasValue)
                query = query;
            if (queryParam.EmpresaParceiraId.HasValue)
                query = query.Where(i => i.CnpjEmpresaCobranca == queryParam.CnpjEmpresaParceira);
            if (queryParam.DataInicial.HasValue)
                query = query.Where(i => i.DataBaixa.Date <= queryParam.DataInicial.Value.Date);
            if (queryParam.DataFinal.HasValue)
                query = query.Where(i => i.DataBaixa.Date <= queryParam.DataFinal.Value.Date);
            
            return query;
        }

        public static IQueryable<TModel> BuscarItems<TModel>(
            this IQueryable<TModel> query, DateTime dtBaixa, string cnpj, BaixaCobrancaQueryParam queryParam)
            where TModel : BaseItensModel
        {
            return query.FiltrarItensBaixaPagamento(queryParam)
                .Where(i => i.DataBaixa == dtBaixa && i.CnpjEmpresaCobranca == cnpj);
        }
    }
}