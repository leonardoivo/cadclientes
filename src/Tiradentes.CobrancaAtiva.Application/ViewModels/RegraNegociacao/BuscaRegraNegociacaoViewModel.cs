using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Semestre;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.SituacaoAluno;
using Tiradentes.CobrancaAtiva.Application.ViewModels.TituloAvulso;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao
{
    public class BuscaRegraNegociacaoViewModel
    {
        public int Id { get; set; }
        public InstituicaoViewModel Instituicao { get; set; }
        public ModalidadeViewModel Modalidade { get; set; }
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

        public IEnumerable<CursoViewModel> Cursos { get; set; }
        public IEnumerable<TituloAvulsoViewModel> TitulosAvulsos { get; set; }
        public IEnumerable<SituacaoAlunoViewModel> SituacoesAlunos { get; set; }
        public IEnumerable<TipoTituloViewModel> TiposTitulos { get; set; }
    }
}