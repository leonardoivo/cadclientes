using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class CursoModel : BaseModel
    {
        public string Descricao { get; set; }

        public int ModalidadeId { get; set; }
        public int InstituicaoId { get; set; }

        public ModalidadeModel Modalidade { get; set; }
        public InstituicaoModel Instituicao { get; set; }

        public ICollection<RegraNegociacaoCursoModel> RegraNegociacaoCurso { get; private set; }
    }
}
