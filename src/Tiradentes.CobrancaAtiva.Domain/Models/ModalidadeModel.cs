using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ModalidadeModel : BaseModel
    {
        public string Modalidade { get; set; }

        public virtual ICollection<CursoModel> Cursos { get; set; }
        public virtual ICollection<InstituicaoModalidadeModel> InstituicoesModalidades { get; set; }
        public ICollection<HonorarioEmpresaParceiraModel> Honorarios { get; private set; }
        public ICollection<RegraNegociacaoModel> RegraNegociacao { get; private set; }
    }
}
