using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao
{
    public class CriarRegraNegociacaoViewModel
    {
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }
        public decimal PercentJurosMulta { get; set; }
        public decimal PercentValor { get; set; }
        public bool Status { get; set; }
        public DateTime MesAnoInicial { get; set; }
        public DateTime MesAnoFinal { get; set; }
        public DateTime ValidadeInicial { get; set; }
        public DateTime ValidadeFinal { get; set; }
        public int[] CursoIds { get; set; }
        public int[] SituacaoAlunoIds { get; set; }
        public int[] TitulosAvulsosId { get; set; }
        public int[] TipoPagamentoIds { get; set; }
        public int[] TipoTituloIds { get; set; }
    }
}
