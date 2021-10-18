namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ParametroEnvioCursoModel : BaseModel
    {
        public ParametroEnvioCursoModel()
        { }
        public ParametroEnvioCursoModel(int cursoId)
        {
            CursoId = cursoId;
        }

        public int CursoId { get; set; }
        public int RegraNegociacaoId { get; set; }

        public CursoModel Curso { get; set; }
        public ParametroEnvioModel ParametroEnvio { get; set; }
    }
}
