using System;

namespace Tiradentes.CobrancaAtiva.Domain.Collections
{
    public class RespostaRegistroTipo2
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
