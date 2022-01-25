namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class HonorarioFaixaEmpresaParceiraModel : BaseModel
    {
        public HonorarioFaixaEmpresaParceiraModel()
        { }

        public int VencidosMaiorQue { get; set; }
        public int VencidosAte { get; set; }
        public float PercentualJuros { get; set; }
        public float PercentualMulta { get; set; }
        public float PercentualRecebimentoAluno { get; set; }

        public int HonorarioEmpresaParceiraId { get; private set; }

        public HonorarioEmpresaParceiraModel HonorarioEmpresaParceira { get; private set; }
    }
}
