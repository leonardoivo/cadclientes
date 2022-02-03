using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class BancoMagisterModel : BaseModel
    {
        public BancoMagisterModel()
        { }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Digito { get; set; }
        public string ContaContabil { get; set; }
    }
}
