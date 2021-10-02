using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Semestre;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.SituacaoAluno;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao
{
    public class BuscaRegraNegociacaoViewModel
    {
        public int Id { get; set; }
        public string Instituicao { get; set; }
        public string Modedalidade { get; set; }
        public decimal PercentJurosMulta { get; set; }
        public decimal PercentValor { get; set; }
        public bool Status { get; set; }
        public DateTime Validade { get; set; }

        public IEnumerable<CursoViewModel> Cursos { get; set; }
        public IEnumerable<SemestreViewModel> Semestres { get; set; }
        public IEnumerable<SituacaoAlunoViewModel> SituacoesAlunos { get; set; }
        public IEnumerable<TipoPagamentoViewModel> TiposPagamentos { get; set; }
        public IEnumerable<TipoTituloViewModel> TiposTitulos { get; set; }
    }
}