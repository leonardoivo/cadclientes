using System;
using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Application.QueryParams
{
    public class ConsultaBaixaCobrancaQueryParam : BasePaginacaoQueryParam
    {
        public IList<int> EmpresaParceiraId { get; set; }
        public IList<int> InstituicaoId { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public bool? Erro { get; set; }
    }
}