namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class RegraNegociacaoSemestreModel : BaseModel
    {
        public int SemestreId { get; set; }
        public int RegraNegociacaoId { get; set; }

        public SemestreModel Semestre { get; set; }
        public RegraNegociacaoModel RegraNegociacao { get; set; }
    }
}
