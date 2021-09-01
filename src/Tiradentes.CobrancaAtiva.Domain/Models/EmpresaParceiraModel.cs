namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class EmpresaParceiraModel : BaseModel
    {
        public EmpresaParceiraModel() { }

        public EmpresaParceiraModel(string nomeFantasia)
        {
            NomeFantasia = nomeFantasia;
        }

        public string NomeFantasia { get; set; }
    }
}
