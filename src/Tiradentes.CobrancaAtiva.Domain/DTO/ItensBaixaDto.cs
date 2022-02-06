using System;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class ItensBaixaDto
    {
        public DateTime DataBaixa { get; set; }
        public string CNPJ { get; set; }
        public int Tipo { get; set; } 
    }
}