using System;

namespace Tiradentes.CobrancaAtiva.Application.QueryParams
{
    public class ConsultaConflitoQueryParam : BasePaginacaoQueryParam
    {
        public int[] EmpresasParceiraTentativa { get; set; }

        public int[] EmpresasParceiraEnvio { get; set; }

        public string NomeLote { get; set; }

        public string NomeAluno { get; set; }

        public string CPF { get; set; }
    }
}
