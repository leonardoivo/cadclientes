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
        public decimal PercentJurosMultaAVista { get; set; }
        public decimal PercentValorAVista { get; set; }

        public decimal PercentJurosMultaCartao { get; set; }
        public decimal PercentValorCartao { get; set; }
        public int QuantidadeParcelasCartao { get; set; }

        public decimal PercentJurosMultaBoleto { get; set; }
        public decimal PercentValorBoleto { get; set; }
        public decimal PercentEntradaBoleto { get; set; }
        public int QuantidadeParcelasBoleto { get; set; }
        public bool Status { get; set; }
        public DateTime InadimplenciaInicial { get; set; }
        public DateTime InadimplenciaFinal { get; set; }
        public DateTime ValidadeInicial { get; set; }
        public DateTime ValidadeFinal { get; set; }

        public IEnumerable<CursoModel> Cursos { get; set; }
        public IEnumerable<TituloAvulsoModel> TitulosAvulsos { get; set; }
        public IEnumerable<SituacaoAlunoModel> SituacoesAlunos { get; set; }
        public IEnumerable<TipoTituloModel> TiposTitulos { get; set; }
    }
}