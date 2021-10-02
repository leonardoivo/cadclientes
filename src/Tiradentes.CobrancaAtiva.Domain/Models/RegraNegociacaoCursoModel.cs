namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class RegraNegociacaoCursoModel : BaseModel
    {
        public RegraNegociacaoCursoModel()
        { }
        public RegraNegociacaoCursoModel(int cursoId)
        {
            CursoId = cursoId;
        }

        public int CursoId { get; set; }
        public int RegraNegociacaoId { get; set; }

        public CursoModel Curso { get; set; }
        public RegraNegociacaoModel RegraNegociacao { get; set; }
    }
}
