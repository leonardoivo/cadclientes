using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class SituacaoAlunoModel : BaseModel
    {
        public string Situacao { get; set; }
        public string CodigoMagister { get; set; }

        public ICollection<RegraNegociacaoSituacaoAlunoModel> RegraNegociacaoSituacaoAluno { get; private set; }
        public ICollection<ParametroEnvioSituacaoAlunoModel> ParametroEnvioSituacaoAluno { get; private set; }
    }
}
