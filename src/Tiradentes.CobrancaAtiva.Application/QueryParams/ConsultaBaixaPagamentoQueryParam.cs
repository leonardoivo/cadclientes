using System;

namespace Tiradentes.CobrancaAtiva.Application.QueryParams
{
    public class ConsultaBaixaPagamentoQueryParam : BasePaginacaoQueryParam
    {
        public string Acordo { get; set; }

        public string Cpf { get; set; }

        public string Matricula { get; set; }

        public string NomeAluno { get; set; }
    }
}
