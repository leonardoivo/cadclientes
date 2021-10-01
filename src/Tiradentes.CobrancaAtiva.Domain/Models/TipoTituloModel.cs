using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class TipoTituloModel : BaseModel
    {
        public string TipoTitulo { get; set; }

        public ICollection<RegraNegociacaoTipoTituloModel> RegraNegociacaoTipoTitulo { get; private set; }
    }
}
