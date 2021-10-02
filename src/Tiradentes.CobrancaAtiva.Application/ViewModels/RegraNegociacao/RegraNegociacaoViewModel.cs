using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao
{
    public class RegraNegociacaoViewModel
    {
        public int InstituicaoId { get; set; }
        public int ModedalidadeId { get; set; }
        public decimal PercentJurosMulta { get; set; }
        public decimal PercentValor { get; set; }
        public bool Status { get; set; }
        public DateTime Validade { get; set; }
    }
}
