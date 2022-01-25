using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class HonorarioEmpresaParceiraModel : BaseModel
    {
        public HonorarioEmpresaParceiraModel()
        { }

        public float PercentualCobrancaIndevida { get; set; }
        public float ValorCobrancaIndevida { get; set; }
        public int FaixaEspecialVencidosMaiorQue { get; set; }
        public int FaixaEspecialVencidosAte { get; set; }
        public float FaixaEspecialPercentualJuros { get; set; }
        public float FaixaEspecialPercentualMulta { get; set; }
        public float FaixaEspecialPercentualRecebimentoAluno { get; set; }

        public int EmpresaParceiraId { get; private set; }

        public EmpresaParceiraModel EmpresaParceira { get; private set; }

        public ICollection<HonorarioFaixaEmpresaParceiraModel> Faixas { get; private set; }
    }
}
