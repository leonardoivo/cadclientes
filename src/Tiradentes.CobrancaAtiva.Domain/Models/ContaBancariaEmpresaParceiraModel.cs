namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ContaBancariaEmpresaParceiraModel : BaseModel
    {
        public ContaBancariaEmpresaParceiraModel()
        { }

        public ContaBancariaEmpresaParceiraModel(int Id,
            string ContaCorrente,
            string CodigoAgencia,
            string Convenio,
            string Pix,
            int BancoId)
        { 
            this.Id = Id;
            this.ContaCorrente = ContaCorrente;
            this.CodigoAgencia = CodigoAgencia;
            this.Convenio = Convenio;
            this.Pix = Pix;
            this.BancoId = BancoId;
        }

        public string ContaCorrente { get; private set; }
        public string CodigoAgencia { get; private set; }
        public string Convenio { get; private set; }
        public string Pix { get; private set; }

        public int EmpresaId { get; private set; }
        public int BancoId { get; private set; }

        public EmpresaParceiraModel Empresa { get; private set; }
        public BancoModel Banco { get; private set; }
    }
}