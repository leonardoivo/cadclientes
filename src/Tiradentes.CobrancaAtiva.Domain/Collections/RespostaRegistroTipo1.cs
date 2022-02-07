using System;

namespace Tiradentes.CobrancaAtiva.Domain.Collections
{
    public class RespostaRegistroTipo1
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
