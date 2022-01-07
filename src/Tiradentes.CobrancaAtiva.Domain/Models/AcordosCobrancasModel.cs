using System;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class AcordosCobrancasModel : BaseModel
    {
        public string CnpjEmpresaCobranca {get;set;}
        public decimal NumeroAcordo { get; set; }
        public DateTime DataBaixa { get; set; }
        public DateTime Data { get; set; }
        public decimal TotalParcelas { get; set; }
        public decimal ValorTotal {get;set;}
        public decimal Mora { get; set; }
        public decimal Multa { get; set; }
        public decimal Saldo_Devedor { get; set; }
        public decimal? Matricula { get; set; }
        public string CPF { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
    }
}
