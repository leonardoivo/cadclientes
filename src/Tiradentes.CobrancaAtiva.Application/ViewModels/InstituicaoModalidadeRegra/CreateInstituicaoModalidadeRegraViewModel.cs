using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.InstituicaoModalidadeRegra
{
    public class CreateInstituicaoModalidadeRegraViewModel
    {
        public int Id { get; set; }
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }

        public DateTime MesAno { get; private set; } 
        public float PercentualDescontoJurosMulta { get; private set; }
        public float PercentualDescontoValorPrincipal { get; private set; }
        public DateTime ValidadeNegociacao { get; private set; }
    }
}
