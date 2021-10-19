namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ParametroEnvioSituacaoAlunoModel : BaseModel
    {
        public ParametroEnvioSituacaoAlunoModel()
        {
        }

        public ParametroEnvioSituacaoAlunoModel(int situacaoAlunoId)
        {
            SituacaoAlunoId = situacaoAlunoId;
        }

        public int SituacaoAlunoId { get; set; }
        public int ParametroEnvioId { get; set; }

        public SituacaoAlunoModel SituacaoAluno { get; set; }
        public ParametroEnvioModel ParametroEnvio { get; set; }
    }
}
