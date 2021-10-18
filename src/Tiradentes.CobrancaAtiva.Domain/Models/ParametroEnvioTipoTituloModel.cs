namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ParametroEnvioTipoTituloModel : BaseModel
    {
        public ParametroEnvioTipoTituloModel()
        {
        }

        public ParametroEnvioTipoTituloModel(int tipoTituloId)
        {
            TipoTituloId = tipoTituloId;
        }

        public int TipoTituloId { get; set; }
        public int RegraNegociacaoId { get; set; }

        public TipoTituloModel TipoTitulo { get; set; }
        public ParametroEnvioModel ParametroEnvio { get; set; }
    }
}
