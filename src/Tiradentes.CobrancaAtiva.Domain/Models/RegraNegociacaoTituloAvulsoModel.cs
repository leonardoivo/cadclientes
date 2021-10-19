namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class RegraNegociacaoTituloAvulsoModel : BaseModel
    {
        public RegraNegociacaoTituloAvulsoModel()
        { }
        public RegraNegociacaoTituloAvulsoModel(int semestreId)
        {
            TituloAvulsoId = semestreId;
        }

        public int TituloAvulsoId { get; set; }
        public int RegraNegociacaoId { get; set; }

        public TituloAvulsoModel TituloAvulso { get; set; }
        public RegraNegociacaoModel RegraNegociacao { get; set; }
    }
}
