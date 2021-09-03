namespace Tiradentes.CobrancaAtiva.Domain.QueryParams
{
    public class EmpresaParceiraQueryParam : BasePaginacaoQueryParam
    {
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string NumeroContrato { get; set; }
        public string AditivoContrato { get; set; }
        public string Contato { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public bool? Status { get; set; }
    }
}
