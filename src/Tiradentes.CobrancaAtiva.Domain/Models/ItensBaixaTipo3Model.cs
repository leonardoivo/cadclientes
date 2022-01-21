using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ItensBaixaTipo3Model : BaseModel
    {
        public DateTime DataBaixa { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Sequencia { get; set; }
        public decimal? CodigoErro { get; set; }
        public decimal NumeroLinha { get; set; }
        public decimal NumeroAcordo { get; set; }
        public decimal Matricula { get; set; }
        public decimal Parcela { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
        public string Tipo_Pagamento { get; set; }
    }
}
