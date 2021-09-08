using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ModalidadeModel : BaseModel
    {
        public string Modalidade { get; set; }

        public virtual ICollection<InstituicaoModalidadeModel> InstituicoesModalidades { get; set; }
    }
}
