using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class SituacaoAlunoModel : BaseModel
    {
        public string Situacao { get; set; }

        public ICollection<RegraNegociacaoSituacaoAlunoModel> RegraNegociacaoSituacaoAluno { get; private set; }
    }
}
