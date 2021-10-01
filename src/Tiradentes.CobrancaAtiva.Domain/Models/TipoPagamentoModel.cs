using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class TipoPagamentoModel : BaseModel
    {
        public string TipoPagamento { get; set; }

        public ICollection<RegraNegociacaoTipoPagamentoModel> RegraNegociacaoTipoPagamento { get; private set; }
    }
}
