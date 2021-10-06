using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BuscaRegraNegociacao 
    {
        public int Id { get; set; }
        public InstituicaoModel Instituicao { get; set; }
        public ModalidadeModel Modalidade { get; set; }
        public decimal PercentJurosMulta { get; set; }
        public decimal PercentValor { get; set; }
        public bool Status { get; set; }
        public DateTime MesAnoInicial { get; set; }
        public DateTime MesAnoFinal { get; set; }
        public DateTime ValidadeInicial { get; set; }
        public DateTime ValidadeFinal { get; set; }

        public IEnumerable<CursoModel> Cursos { get; set; }
        public IEnumerable<SemestreModel> Semestres { get; set; }
        public IEnumerable<SituacaoAlunoModel> SituacoesAlunos { get; set; }
        public IEnumerable<TipoPagamentoModel> TiposPagamentos { get; set; }
        public IEnumerable<TipoTituloModel> TiposTitulos { get; set; }
    }
}