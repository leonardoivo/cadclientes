using System;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class BaseItensModel : BaseModel
    {
        public DateTime DataBaixa { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
    }
}