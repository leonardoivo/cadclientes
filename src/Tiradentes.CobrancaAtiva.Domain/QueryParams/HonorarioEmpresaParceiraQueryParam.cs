namespace Tiradentes.CobrancaAtiva.Domain.QueryParams
{
    public class HonorarioEmpresaParceiraQueryParam : BasePaginacaoQueryParam
    {
        public int EmpresaParceiraId { get; set; }
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }
    }
}
