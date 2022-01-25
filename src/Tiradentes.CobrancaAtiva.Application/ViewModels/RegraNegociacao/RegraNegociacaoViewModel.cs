using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao
{
    public class RegraNegociacaoViewModel
    {
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }
        public decimal PercentJurosMulta { get; set; }
        public decimal PercentValor { get; set; }
        public bool Status { get; set; }

        public DateTime ValidadeInicial { get; private set; }
        public DateTime ValidadeFinal { get; private set; }
    }
}
