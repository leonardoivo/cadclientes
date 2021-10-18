namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ParametroEnvioTituloAvulsoModel : BaseModel
    {
        public ParametroEnvioTituloAvulsoModel()
        {
        }

        public ParametroEnvioTituloAvulsoModel(int tipoTituloId)
        {
            TipoTituloId = tipoTituloId;
        }

        public int TipoTituloId { get; set; }
        public int RegraNegociacaoId { get; set; }

        public TipoTituloModel TipoTitulo { get; set; }
        public ParametroEnvioModel ParametroEnvio { get; set; }
    }
}
