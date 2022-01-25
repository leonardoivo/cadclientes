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
        public float PercentualCobrancaIndevida { get; set; }
        public float ValorCobrancaIndevida { get; set; }
        public int FaixaEspecialVencidosMaiorQue { get; set; }
        public int FaixaEspecialVencidosAte { get; set; }
        public float FaixaEspecialPercentualJuros { get; set; }
        public float FaixaEspecialPercentualMulta { get; set; }
        public float FaixaEspecialPercentualRecebimentoAluno { get; set; }
        
        public ICollection<CreateHonorarioFaixaEmpresaParceiraViewModel> Faixas { get; set; }
    }
}
