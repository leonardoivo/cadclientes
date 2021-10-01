using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao
{
    public class BuscaRegraNegociacaoViewModel
    {
        public string Instituicao { get; set; }
        public string Modedalidade { get; set; }
        public decimal PercentJurosMulta { get; set; }
        public decimal PercentValor { get; set; }
        public bool Status { get; set; }
        public DateTime Validade { get; set; }

        public IEnumerable<CursoViewModel> Cursos { get; set; }
    }
}