namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ParametroEnvioTituloAvulsoModel : BaseModel
    {
        public ParametroEnvioTituloAvulsoModel()
        {
        }

        public ParametroEnvioTituloAvulsoModel(int tituloAvulsoId)
        {
            TituloAvulsoId = tituloAvulsoId;
        }

        public int TituloAvulsoId { get; set; }
        public int ParametroEnvioId { get; set; }

        public TituloAvulsoModel TituloAvulso { get; set; }
        public ParametroEnvioModel ParametroEnvio { get; set; }
    }
}
