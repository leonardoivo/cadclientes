using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels
{
    public class ViewModelPaginada<TViewModel>
    {
        public IList<TViewModel> Items { get; set; }
        public int TotalItems { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }
    }
}
