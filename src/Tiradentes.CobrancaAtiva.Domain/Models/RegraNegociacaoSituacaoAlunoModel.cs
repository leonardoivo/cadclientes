namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class RegraNegociacaoSituacaoAlunoModel : BaseModel
    {
        public int SituacaoAlunoId { get; set; }
        public int RegraNegociacaoId { get; set; }

        public SituacaoAlunoModel SituacaoAluno { get; set; }
        public RegraNegociacaoModel RegraNegociacao { get; set; }
    }
}
