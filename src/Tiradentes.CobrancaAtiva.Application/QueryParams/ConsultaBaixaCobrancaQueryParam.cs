using System;

namespace Tiradentes.CobrancaAtiva.Application.QueryParams
{
    public class ConsultaBaixaCobrancaQueryParam : BasePaginacaoQueryParam
    {
        public int? EmpresaParceiraId { get; set; }
        public int? InstituicaoId { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}
