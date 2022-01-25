using System;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ItensGeracaoModel : BaseModel
    {
        public string DataGeracao { get; set; }
        public string Matricula { get; set; }
        public string Periodo { get; set; }
        public int Parcela { get; set; }
        public string DataVencimento { get; set; }
        public float Valor { get; set; }
        public string Controle { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
        public string DescricaoInadimplencia { get; set; }
        public string PeriodoChequeDevolvido { get; set; }
    }
}