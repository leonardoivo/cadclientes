using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira
{
    public class HonorarioEmpresaParceiraViewModel
    {
        public int Id { get; set; }
        public EmpresaParceiraViewModel EmpresaParceira { get; set; }
        public InstituicaoViewModel Instituicao { get; set; }
        public ModalidadeViewModel Modalidade { get; set; }

        public float PercentualCobrancaIndevida { get; set; }
        public float ValorCobrancaIndevida { get; set; }
        public float PercentualNegociacaoEmpresaParceira { get; set; }
        public float ValorNegociacaoEmpresaParceira { get; set; }
        public float PercentualNegociacaoInstituicaoEnsino { get; set; }
        public float ValorNegociacaoInstituicaoEnsino { get; set; }
        public string Informacao { get; set; }
    }
}
