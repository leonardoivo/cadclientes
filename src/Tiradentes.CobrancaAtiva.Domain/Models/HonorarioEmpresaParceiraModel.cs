namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class HonorarioEmpresaParceiraModel : BaseModel
    {
        public HonorarioEmpresaParceiraModel()
        { }

        public float PercentualNegociacaoEmpresaParceira { get; private set; }
        public float ValorNegociacaoEmpresaParceira { get; private set; }
        public float ValorNegociacaoInstituicaoEnsino { get; private set; } 
        public float PercentualNegociacaoInstituicaoEnsino { get; private set; } 
        public float PercentualCobrancaIndevida { get; private set; }
        public float ValorCobrancaIndevida { get; private set; }
        public string Informacao { get; private set; }

        public int EmpresaParceiraId { get; private set; }
        public int InstituicaoId { get; private set; }
        public int ModalidadeId { get; private set; }

        public EmpresaParceiraModel EmpresaParceira { get; private set; }
        public InstituicaoModel InstituicaoModel { get; private set; }
        public ModalidadeModel ModalidadeModel { get; private set; }
    }
}
