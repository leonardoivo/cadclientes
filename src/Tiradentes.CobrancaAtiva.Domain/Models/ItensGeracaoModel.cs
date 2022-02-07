using System;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ItensGeracaoModel : BaseModel
    {
        public DateTime DataGeracao { get; set; }
        public decimal Matricula { get; set; }
        public decimal Periodo { get; set; }
        public decimal Parcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public string Controle { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
        public string DescricaoInadimplencia { get; set; }
        public string PeriodoOutros { get; set; }
        public string PeriodoChequeDevolvido { get; set; }
    }
}