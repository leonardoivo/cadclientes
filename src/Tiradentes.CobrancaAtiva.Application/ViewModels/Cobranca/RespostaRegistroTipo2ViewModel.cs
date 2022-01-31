using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca
{
    public class RespostaRegistroTipo2ViewModel
    {
        public DateTime DataVencimentoParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal SaldoDevedorTotal { get; set; }
        public string Produto { get; set; }
        public string DescricaoProduto { get; set; }
        public string Fase { get; set; }
        public string CodigoControleCliente { get; set; }

    }
}
