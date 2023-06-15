namespace GestaoClientes.Domain.QueryParams
{
    public class BasePaginacaoQueryParam
    {
        public int Pagina { get; set; }
        public int Limite { get; set; }
        public string OrdenarPor { get; set; }
        public string SentidoOrdenacao { get; set; }
    }
}
