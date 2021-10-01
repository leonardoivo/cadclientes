using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao
{
    public class CriarRegraNegociacaoViewModel
    {
        public int InstituicaoId { get; set; }
        public int ModedalidadeId { get; set; }
        public decimal PercentJurosMulta { get; set; }
        public decimal PercentValor { get; set; }
        public bool Status { get; set; }
        public DateTime Validade { get; set; }
        public int[] CursoIds { get; set; }
        public int[] SituacaoAlunoIds { get; set; }
        public int[] SemestreIds { get; set; }
        public int[] TipoPagamentoIds { get; set; }
        public int[] TipoTituloIds { get; set; }
    }
}
