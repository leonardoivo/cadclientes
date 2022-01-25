using System;
namespace Tiradentes.CobrancaAtiva.Domain.QueryParams
{
    public class ConflitoQueryParam : BasePaginacaoQueryParam
    {
        public int[] EmpresasParceiraTentativa { get; set; }

        public int[] EmpresasParceiraEnvio { get; set; }

        public string NomeLote { get; set; }

        public string NomeAluno { get; set; }

        public string CPF { get; set; }

    }
}
