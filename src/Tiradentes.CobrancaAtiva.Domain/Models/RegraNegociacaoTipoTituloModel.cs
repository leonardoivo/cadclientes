namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class RegraNegociacaoTipoTituloModel : BaseModel
    {
        public RegraNegociacaoTipoTituloModel()
        {
        }

        public RegraNegociacaoTipoTituloModel(int tipoTituloId)
        {
            TipoTituloId = tipoTituloId;
        }

        public int TipoTituloId { get; set; }
        public int RegraNegociacaoId { get; set; }

        public TipoTituloModel TipoTitulo { get; set; }
        public RegraNegociacaoModel RegraNegociacao { get; set; }
    }
}
