using System;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public static class BuscaItensBaixaCobranca
    {
        public static IQueryable<ItensBaixaDto> SelectItensBaixaPagamento<TModel>(
            this IQueryable<TModel> query, int tipo) where TModel : BaseItensModel
        {
            return query.Select(i => new ItensBaixaDto
            {
                DataBaixa = i.DataBaixa,
                CNPJ = i.CnpjEmpresaCobranca,
                Tipo = tipo
            });
        }

        public static IQueryable<TModel> FiltrarItensBaixaPagamento<TModel>(
            this IQueryable<TModel> query) where TModel : BaseItensModel
        {
            return query.Where(i => i.CnpjEmpresaCobranca != null);
        }

        public static IQueryable<TModel> BuscarItems<TModel>(
            this IQueryable<TModel> query, DateTime dtBaixa, string cnpj) where TModel : BaseItensModel
        {
            return query.FiltrarItensBaixaPagamento()
                .Where(i => i.DataBaixa == dtBaixa && i.CnpjEmpresaCobranca == cnpj);
        }
    }
}