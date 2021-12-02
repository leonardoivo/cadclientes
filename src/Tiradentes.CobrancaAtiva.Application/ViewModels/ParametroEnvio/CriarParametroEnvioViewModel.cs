using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio
{
    public class CriarParametroEnvioViewModel
    {
        public int EmpresaParceiraId { get; set; }
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }
        public int? DiaEnvio { get; set; }
        public bool Status { get; set; }
        public DateTime InadimplenciaInicial { get; set; }
        public DateTime InadimplenciaFinal { get; set; }
        public DateTime ValidadeInicial { get; set; }
        public DateTime ValidadeFinal { get; set; }
        public int[] CursoIds { get; set; }
        public int[] SituacaoAlunoIds { get; set; }
        public int[] TituloAvulsoIds { get; set; }
        public int[] TipoTituloIds { get; set; }
    }
}
