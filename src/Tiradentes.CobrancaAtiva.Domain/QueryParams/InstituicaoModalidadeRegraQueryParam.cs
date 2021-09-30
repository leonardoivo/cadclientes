namespace Tiradentes.CobrancaAtiva.Domain.QueryParams
{
    public class InstituicaoModalidadeRegraQueryParam : BasePaginacaoQueryParam
    {
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }
    }
}
