using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class ModelPaginada<TModel>
    {
        public ModelPaginada()
        {
            Items = new List<TModel>();
        }

        public IList<TModel> Items { get; set; }
        public int TotalItems { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }
    }
}
