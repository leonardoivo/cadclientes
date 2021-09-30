namespace Tiradentes.CobrancaAtiva.Application.QueryParams
{
    public class ConsultaInstituicaoModalidadeRegraQueryParam : BasePaginacaoQueryParam
    {
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }
    }
}
