using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class CursoModel : BaseModel
    {
        public string Descricao { get; set; }

        public ICollection<RegraNegociacaoCursoModel> RegraNegociacaoCurso { get; private set; }
    }
}
