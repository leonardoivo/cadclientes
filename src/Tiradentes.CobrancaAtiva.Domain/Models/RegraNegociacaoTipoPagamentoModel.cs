namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class RegraNegociacaoTipoPagamentoModel : BaseModel
    {
        public RegraNegociacaoTipoPagamentoModel()
        {
        }

        public RegraNegociacaoTipoPagamentoModel(int tipoPagamentoId)
        {
            TipoPagamentoId = tipoPagamentoId;
        }

        public int TipoPagamentoId { get; set; }
        public int RegraNegociacaoId { get; set; }

        public TipoPagamentoModel TipoPagamento { get; set; }
        public RegraNegociacaoModel RegraNegociacao { get; set; }
    }
}
