using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca
{
    public class RespostaRegistroTipo1ViewModel
    {
        public decimal JurosParcela { get; set; }
        public decimal MultaParcela { get; set; }
        public decimal ValorTotalParcela { get; set; }
        public DateTime DataFechamentoAcordo { get; set; }
        public int TotalParcelasAcordo { get; set; }
        public DateTime DataVencimentoParcela { get; set; }
        public decimal ValorParcela { get; set; }
    }
}
