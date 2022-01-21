using System;


namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ParcelasTitulosModel : BaseModel
    {
        public string CnpjEmpresaCobranca { get; set; }
        public Int64 NumeroAcordo { get; set; }
        public Int64 Matricula { get; set; }
        public decimal Periodo { get; set; }        
        public decimal PeriodoChequeDevolvido { get; set; }        
        public decimal Parcela { get; set; }        
        public string Sistema { get; set; }        
        public string TipoInadimplencia { get; set; }
        public DateTime DataBaixa { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
    }
}
