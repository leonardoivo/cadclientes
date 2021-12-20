using System;
namespace Tiradentes.CobrancaAtiva.Domain.QueryParams
{
    public class ConflitoQueryParam : BasePaginacaoQueryParam
    {
        public int[] EmpresasParceiraTentativa { get; set; }

        public int[] EmpresasParceiraEnvio { get; set; }

        public string NomeLote { get; set; }

        public int[] Matriculas { get; set; }

        public string NomeAluno { get; set; }

        public string CPF { get; set; }

        public int[] Modalidades { get; set; }

        public string Parcela { get; set; }

        public decimal? Valor { get; set; }

        public DateTime? DataEnvio { get; set; }
    }
}
