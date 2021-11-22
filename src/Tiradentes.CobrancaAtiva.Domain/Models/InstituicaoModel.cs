using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class InstituicaoModel : BaseModel
    {
        public string Instituicao { get; set; }

        public virtual ICollection<CursoModel> Cursos { get; set; }
        public virtual ICollection<InstituicaoModalidadeModel> InstituicoesModalidades { get; set; }
        public ICollection<HonorarioEmpresaParceiraModel> Honorarios { get; private set; }
        public ICollection<RegraNegociacaoModel> RegraNegociacao { get; private set; }
        public ICollection<ParametroEnvioModel> ParametroEnvios { get; private set; }
    }
}
