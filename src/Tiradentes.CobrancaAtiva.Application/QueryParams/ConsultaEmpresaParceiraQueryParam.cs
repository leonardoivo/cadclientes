namespace Tiradentes.CobrancaAtiva.Application.QueryParams
{
    public class ConsultaEmpresaParceiraQueryParam : BasePaginacaoQueryParam
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
