using System;

namespace Tiradentes.CobrancaAtiva.Application.QueryParams
{
    public class ConsultaRegraNegociacaoQueryParam : BasePaginacaoQueryParam
    {
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }
        public decimal PercentJurosMulta { get; set; }
        public decimal PercentValor { get; set; }
        public bool? Status { get; set; }
        public DateTime? MesAnoInicial { get; set; }
        public DateTime? MesAnoFinal { get; set; }
        public DateTime? ValidadeInicial { get; set; }
        public DateTime? ValidadeFinal { get; set; }
        public int[] Cursos { get; set; }
        public int[] TitulosAvulsos { get; set; }
        public int[] TiposPagamentos { get; set; }
        public int[] SituacoesAlunos { get; set; }
        public int[] TiposTitulos { get; set; }
    }
}
