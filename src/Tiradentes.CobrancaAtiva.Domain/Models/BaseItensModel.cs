using System;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class BaseItensModel : BaseModel
    {
        public DateTime DataBaixa { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
        public long Matricula { get; set; }
        public long NumeroAcordo { get; set; }
        public long? NumeroLinha { get; set; }
        public int? CodigoErro { get; set; }
        public int Parcela { get; set; }

    }
}