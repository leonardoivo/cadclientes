namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ContatoEmpresaParceiraModel : BaseModel
    {
        public string Contato { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public int EmpresaId { get; private set; }

        public EmpresaParceiraModel Empresa { get; private set; }
    }
}
