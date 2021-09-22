using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira
{
    public class CreateHonorarioEmpresaParceiraViewModel
    {
        public int Id { get; set; }
        public int EmpresaParceiraId { get; set; }
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }

        public float PercentualNegociacaoEmpresaParceira { get; set; }
        public float ValorNegociacaoEmpresaParceira { get; set; } 
        public float PercentualNegociacaoEmpresaCobranca { get; set; }
        public float ValorNegociacaoInstituicaoEnsino { get; set; } 
        public float PercentualNegociacaoInstituicaoEnsino { get; set; } 
        public float CobrancaIndevida { get; set; }
        public string Informacao { get; set; }
    }
}
