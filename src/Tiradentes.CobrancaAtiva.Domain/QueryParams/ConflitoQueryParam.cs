using System;
namespace Tiradentes.CobrancaAtiva.Domain.QueryParams
{
    public class ConflitoQueryParam : BasePaginacaoQueryParam
    {
        public string EmpresaParceiraIn { get; set; }

        public string EmpresaParceiraOu { get; set; }

        public string NomeLote { get; set; }

        public int Matricula { get; set; }

        public string Nomealuno { get; set; }

        public string CPF { get; set; }

        public string ModalidadeEnsino { get; set; }

        public string ParcelaConflito { get; set; }

        public float ValorConflito { get; set; }

        public bool SituacaoConflito { get; set; }

        public DateTime? DataEnvio { get; set; }
    }
}
