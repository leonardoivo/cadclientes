namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ContatoEmpresaParceiraModel : BaseModel
    {
        public string Contato { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public EmpresaParceiraModel EmpresaParceira { get; set; }
    }
}
