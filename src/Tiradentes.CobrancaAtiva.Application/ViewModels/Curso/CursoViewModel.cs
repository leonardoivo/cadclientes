using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Curso
{
    public class CursoViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public ModalidadeViewModel Modalidade { get; set; }
        public InstituicaoViewModel Instituicao { get; set; }
    }
}