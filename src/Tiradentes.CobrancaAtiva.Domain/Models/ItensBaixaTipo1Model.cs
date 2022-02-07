using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ItensBaixaTipo1Model : BaseItensModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Sequencia { get; set; }
        public decimal Multa { get; set; }
        public decimal Juros { get; set; }
        public DateTime DataVencimento { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
        public decimal Valor { get; set; }
    }
}
