namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ArquivoCobrancasModel : BaseModel
    {
        public string DataGeracao { get; set; }
        public int Sequencia { get; set; }
        public string Arquivo { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
        public string NomeLote { get; set; }
    }
}