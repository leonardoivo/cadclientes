namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class HonorarioEmpresaParceiraModel : BaseModel
    {
        public HonorarioEmpresaParceiraModel()
        { }

        public float ValorMaiorNegociacaoEmpresaParceira { get; private set;} 
        public float ValorMenorIgualNegociacaoEmpresaParceira {get;private set; }
        public float ValorMaiorNegociacaoIES { get; private set; } 
        public float ValorMenorIgualNegociacaoIES { get; private set; } 
        public float CobrancaIndevida { get; private set; }
        public string Informacao { get; private set; }


        public int InstituicaoId { get; private set; }
        public int InstituicaoModalidadeId { get; private set; }

        public InstituicaoModel InstituicaoModel { get; private set; }
        public InstituicaoModalidadeModel InstituicaoModalidadeModel { get; private set; }
    }
}
