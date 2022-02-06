using System;
using System.Collections;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BuscaBaixaPagamentoDto
    {
        public string CNPJ { get; set; }
        public DateTime DataBaixa { get; set; }
        public IEnumerable t { get; set; }
    }
}