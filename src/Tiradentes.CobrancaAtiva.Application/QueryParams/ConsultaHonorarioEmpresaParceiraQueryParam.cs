namespace Tiradentes.CobrancaAtiva.Application.QueryParams
{
    public class ConsultaHonorarioEmpresaParceiraQueryParam : BasePaginacaoQueryParam
    {
        public int EmpresaParceiraId { get; set; }
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }
    }
}
