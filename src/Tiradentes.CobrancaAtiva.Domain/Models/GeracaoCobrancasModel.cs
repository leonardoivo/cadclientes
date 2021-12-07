using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class GeracaoCobrancasModel : BaseModel
    {
        public string DataGeracao { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string Username { get; set; }
        public string DataHoraEnvio { get; set; }
        public string Sistema { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
        public string TipoInadimplencia { get; set; }
    }
}