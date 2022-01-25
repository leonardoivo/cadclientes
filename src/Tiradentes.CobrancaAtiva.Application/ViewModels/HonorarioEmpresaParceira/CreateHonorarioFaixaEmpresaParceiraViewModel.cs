using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira
{
    public class CreateHonorarioFaixaEmpresaParceiraViewModel
    {
        public int Id { get; set; }
        public int HonorarioEmpresaParceiraId { get; private set; }
        public int VencidosMaiorQue { get; set; }
        public int VencidosAte { get; set; }
        public float PercentualJuros { get; set; }
        public float PercentualMulta { get; set; }
        public float PercentualRecebimentoAluno { get; set; }
    }
}
