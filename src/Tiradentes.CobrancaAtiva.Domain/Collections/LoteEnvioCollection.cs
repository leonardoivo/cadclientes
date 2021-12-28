namespace Tiradentes.CobrancaAtiva.Domain.Collections
{
    public class LoteEnvioCollection
    {
        public string Id { get; set; }
        public System.Guid Lote { get; set; }
        public int EmpresaId { get; set; }
        public string CnpjEmpresa { get; set; }
        public string NomeEmpresa { get; set; }
        public int InstituicaoId { get; set; }
        public string Instituicao { get; set; }
        public System.DateTime ValidadeInicial { get; set; }
        public System.DateTime ValidadeFinal { get; set; }
    }
}