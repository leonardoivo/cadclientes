using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BuscaRegraNegociacao 
    {
        public int Id { get; set; }
        public string Instituicao { get; set; }
        public string Modedalidade { get; set; }
        public decimal PercentJurosMulta { get; set; }
        public decimal PercentValor { get; set; }
        public bool Status { get; set; }
        public DateTime Validade { get; set; }

        public IEnumerable<CursoModel> Cursos { get; set; }
        public IEnumerable<SemestreModel> Semestres { get; set; }
        public IEnumerable<SituacaoAlunoModel> SituacoesAlunos { get; set; }
        public IEnumerable<TipoPagamentoModel> TiposPagamentos { get; set; }
        public IEnumerable<TipoTituloModel> TiposTitulos { get; set; }
    }
}