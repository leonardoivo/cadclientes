namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class RegraNegociacaoTipoTituloModel : BaseModel
    {
        public int TipoTituloId { get; set; }
        public int RegraNegociacaoId { get; set; }

        public TipoTituloModel TipoTitulo { get; set; }
        public RegraNegociacaoModel RegraNegociacao { get; set; }
    }
}
