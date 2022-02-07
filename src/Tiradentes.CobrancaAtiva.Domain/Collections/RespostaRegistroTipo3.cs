using System;

namespace Tiradentes.CobrancaAtiva.Domain.Collections
{
    public class RespostaRegistroTipo3
    {
        public string NossoNumero { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataBaixa { get; set; }
        public decimal ValorPago { get; set; }
        public string TipoPagamento { get; set; }
    }
}
