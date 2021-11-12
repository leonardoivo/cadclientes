namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ParametroEnvioModalidadeModel : BaseModel
    {
        public ParametroEnvioModalidadeModel()
        { }
        public ParametroEnvioModalidadeModel(int modalidadeId)
        {
            ModalidadeId = modalidadeId;
        }

        public int ModalidadeId { get; set; }
        public int ParametroEnvioId { get; set; }

        public ModalidadeModel Modalidade { get; set; }
        public ParametroEnvioModel ParametroEnvio { get; set; }
    }
}
