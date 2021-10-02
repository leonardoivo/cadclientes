using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class SemestreModel : BaseModel
    {
        public int Numeral { get; set; }
        public string Descricao { get; set; }

        public ICollection<RegraNegociacaoSemestreModel> RegraNegociacaoSemestre { get; private set; }
    }
}
