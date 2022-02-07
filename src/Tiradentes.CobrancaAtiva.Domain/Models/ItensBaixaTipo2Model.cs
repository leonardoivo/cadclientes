using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ItensBaixaTipo2Model : BaseItensModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Sequencia { get; set; }
        public decimal Periodo { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public string PeriodoOutros { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }

    }
}
