namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ParametroEnvioInstituicaoModel : BaseModel
    {
        public ParametroEnvioInstituicaoModel()
        { }
        public ParametroEnvioInstituicaoModel(int instituicaoId)
        {
            InstituicaoId = instituicaoId;
        }

        public int InstituicaoId { get; set; }
        public int ParametroEnvioId { get; set; }

        public InstituicaoModel Instituicao { get; set; }
        public ParametroEnvioModel ParametroEnvio { get; set; }
    }
}
