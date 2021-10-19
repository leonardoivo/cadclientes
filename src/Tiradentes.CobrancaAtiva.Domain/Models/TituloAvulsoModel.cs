using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class TituloAvulsoModel : BaseModel
    {
        public int CodigoGT { get; set; }
        public string Descricao { get; set; }

        public ICollection<ParametroEnvioTituloAvulsoModel> ParametroEnvioTituloAvulso { get; private set; }
    }
}
