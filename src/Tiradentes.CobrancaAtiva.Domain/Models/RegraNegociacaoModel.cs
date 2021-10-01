using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class RegraNegociacaoModel : BaseModel
    {
        public RegraNegociacaoModel()
        { }

        public int InstituicaoId { get; private set; }
        public int ModedalidadeId { get; private set; }
        public decimal PercentJurosMulta { get; private set; }
        public decimal PercentValor { get; private set; }
        public bool Status { get; private set; }
        public DateTime Validade { get; private set; }

        public InstituicaoModel Instituicao { get; set; }
        public ModalidadeModel Modalidade { get; set; }
        public ICollection<RegraNegociacaoCursoModel> RegraNegociacaoCurso { get; private set; }
        public ICollection<RegraNegociacaoSemestreModel> RegraNegociacaoSemestre { get; private set; }
        public ICollection<RegraNegociacaoSituacaoAlunoModel> RegraNegociacaoSituacaoAluno { get; private set; }
        public ICollection<RegraNegociacaoTipoPagamentoModel> RegraNegociacaoTipoPagamento { get; private set; }
        public ICollection<RegraNegociacaoTipoTituloModel> RegraNegociacaoTipoTitulo { get; private set; }
    }
}
