using System;


namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ParcelasAcordoModel : BaseModel
    {
        public decimal NumeroAcordo { get; set; }        
        public decimal Parcela { get; set; }
        public DateTime? DataBaixa { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataBaixaPagamento { get; set; }
        public decimal Valor { get; set; }
        public decimal? ValorPago { get; set; }
        public string CnpjEmpresaCobranca { get; set; }        
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
    }
}
