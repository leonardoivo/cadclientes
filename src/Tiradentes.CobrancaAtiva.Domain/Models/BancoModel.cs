using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class BancoModel : BaseModel
    {
        public BancoModel()
        { }

        public string Codigo { get; private set; }
        public string Nome { get; private set; }

        public ICollection<ContaBancariaEmpresaParceiraModel> ContaBancarias { get; private set; }
    }
}
