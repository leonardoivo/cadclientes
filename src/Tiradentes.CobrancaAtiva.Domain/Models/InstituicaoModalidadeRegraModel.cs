using System;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class InstituicaoModalidadeRegraModel : BaseModel
    {
        public InstituicaoModalidadeRegraModel()
        { }

        public DateTime MesAno { get; private set; } 
        public float PercentualDescontoJurosMulta { get; private set; }
        public float PercentualDescontoValorPrincipal { get; private set; }
        public DateTime ValidadeNegociacao { get; private set; }

        public int InstituicaoId { get; private set; }
        public int ModalidadeId { get; private set; }

        public InstituicaoModel Instituicao { get; private set; }
        public ModalidadeModel Modalidade { get; private set; }
    }
}
