using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class InstituicaoModel : BaseModel
    {
        public string Instituicao { get; set; }

        public virtual ICollection<ModalidadeModel> Modalidades { get; set; }
    }
}
