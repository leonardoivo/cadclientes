using System;
using System.Collections;
using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BuscaBaixaPagamentoDto
    {
        public string CNPJ { get; set; }
        public DateTime DataBaixa { get; set; }
        public List<ItensBaixaDto> Items { get; set; }
    }
}