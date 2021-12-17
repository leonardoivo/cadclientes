using System;
namespace Tiradentes.CobrancaAtiva.Domain.QueryParams
{
    public class ConflitoQueryParam : BasePaginacaoQueryParam
    {
        public string EmpresaParceiraTentativa { get; set; }

        public string EmpresaParceiraEnvio { get; set; }

        public string NomeLote { get; set; }

        public int Matricula { get; set; }

        public string NomeAluno { get; set; }

        public string CPF { get; set; }

        public string Modalidade { get; set; }

        public string Parcela { get; set; }

        public float Valor { get; set; }

        //public bool SituacaoConflito { get; set; }

        public DateTime? DataEnvio { get; set; }
    }
}
