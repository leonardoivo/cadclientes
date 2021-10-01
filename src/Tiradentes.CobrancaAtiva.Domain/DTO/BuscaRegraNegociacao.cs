using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BuscaRegraNegociacao 
    {
        public string Instituicao { get; set; }
        public string Modedalidade { get; set; }
        public decimal PercentJurosMulta { get; set; }
        public decimal PercentValor { get; set; }
        public bool Status { get; set; }
        public DateTime Validade { get; set; }

        public IEnumerable<CursoModel> Cursos { get; set; }
    }
}