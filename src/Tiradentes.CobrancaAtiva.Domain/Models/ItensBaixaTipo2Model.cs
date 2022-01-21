using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ItensBaixaTipo2Model : BaseModel
    {
        public DateTime DataBaixa { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Sequencia { get; set; }
        public decimal? CodigoErro { get; set; }
        public decimal NumeroLinha { get; set; }
        public decimal NumeroAcordo { get; set; }
        public decimal Matricula { get; set; }
        public decimal Periodo { get; set; }
        public decimal Parcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }

        public string CnpjEmpresaCobranca { get; set; }
        public string PeriodoChequeDevolvido { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }

    }
}
