using System;

namespace Tiradentes.CobrancaAtiva.Domain.QueryParams
{
    public class BaixaCobrancaQueryParam : BasePaginacaoQueryParam
    {
        public int? EmpresaParceiraId { get; set; }
        public int? InstituicaoId { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public string CnpjEmpresaParceira { get; set; }
    }
}