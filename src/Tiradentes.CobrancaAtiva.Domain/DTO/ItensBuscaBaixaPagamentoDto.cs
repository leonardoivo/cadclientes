using System;
using System.Collections;
using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BuscaBaixaPagamentoDto
    {
        public DateTime DataBaixa { get; set; }
        public string CNPJ { get; set; }
        public int Tipo { get; set; } 
    }
}