using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao
{
    public class CriarRegraNegociacaoViewModel
    {
        public int? InstituicaoId { get; set; }
        public int? ModalidadeId { get; set; }
        public decimal PercentJurosMultaAVista { get; set; }
        public decimal PercentValorAVista { get; set; }

        public decimal PercentJurosMultaCartao { get; set; }
        public decimal PercentValorCartao { get; set; }
        public int QuantidadeParcelasCartao { get; set; }

        public decimal PercentJurosMultaBoleto { get; set; }
        public decimal PercentValorBoleto { get; set; }
        public decimal PercentEntradaBoleto { get; set; }
        public int QuantidadeParcelasBoleto { get; set; }
        public bool Status { get; set; }
        public DateTime InadimplenciaInicial { get; set; }
        public DateTime InadimplenciaFinal { get; set; }
        public DateTime ValidadeInicial { get; set; }
        public DateTime ValidadeFinal { get; set; }
        public int[] CursoIds { get; set; }
        public int[] SituacaoAlunoIds { get; set; }
        public int[] TitulosAvulsosId { get; set; }
        public int[] TipoTituloIds { get; set; }
    }
}
