using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ItensBaixaTipo2Model : BaseItensModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Sequencia { get; set; }
        public decimal CodigoErro { get; set; }
        public decimal NumeroLinha { get; set; }
        public Int64 NumeroAcordo { get; set; }
        public Nullable<Int64> Matricula { get; set; }
        public Nullable<decimal> Periodo { get; set; }
        public Nullable<decimal> Parcela { get; set; }
        public Nullable<DateTime> DataVencimento { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public string PeriodoOutros { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }

    }
}
