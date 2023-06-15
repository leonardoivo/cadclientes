using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestaoClientes.Domain.DTO;

namespace GestaoClientes.Infrastructure.Repositories.Helpers
{
    public static class Paginacao
    {

        /// <summary>
        /// Paginação usada em consultas de banco
        /// </summary>
        public static async Task<ModelPaginada<TModel>> PaginarAsync<TModel>(
            this IQueryable<TModel> query,
            int pagina,
            int limite)
        {
            pagina = (pagina < 1) ? 1 : pagina;
            limite = (limite < 1) ? 10 : limite;

            var modelPaginada = new ModelPaginada<TModel>();

            modelPaginada.TotalItems = await query.CountAsync();

            var paginaInicial = (pagina - 1) * limite;

            modelPaginada.Items = await query
                                    .Skip(paginaInicial)
                                    .Take(limite)
                                    .ToListAsync();

            modelPaginada.PaginaAtual = pagina;
            modelPaginada.TamanhoPagina = limite;
            modelPaginada.TotalPaginas = (int)Math.Ceiling(modelPaginada.TotalItems / (double)limite);


            return modelPaginada;
        }

        /// <summary>
        /// Paginação usada em listas ja em memoria
        /// </summary>
        public static ModelPaginada<TModel> Paginar<TModel>(this IQueryable<TModel> query, int pagina, int limite)
        {
            pagina = (pagina < 1) ? 1 : pagina;
            limite = (limite < 1) ? 10 : limite;

            var modelPaginada = new ModelPaginada<TModel>();

            modelPaginada.TotalItems =  query.Count();

            var paginaInicial = (pagina - 1) * limite;

            modelPaginada.Items =  query
                                    .Skip(paginaInicial)
                                    .Take(limite)
                                    .ToList();

            modelPaginada.PaginaAtual = pagina;
            modelPaginada.TamanhoPagina = limite;
            modelPaginada.TotalPaginas = (int)Math.Ceiling(modelPaginada.TotalItems / (double)limite);


            return modelPaginada;
        }
    }
}
