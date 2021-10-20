using System;

namespace Tiradentes.CobrancaAtiva.Domain.QueryParams
{
    public class ParametroEnvioQueryParam : BasePaginacaoQueryParam
    {
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }
        public int EmpresaParceiraId { get; set; }
        public int? DiaEnvio { get; set; }
        public bool? Status { get; set; }
        public DateTime? InadimplenciaInicial { get; set; }
        public DateTime? InadimplenciaFinal { get; set; }
        public DateTime? ValidadeInicial { get; set; }
        public DateTime? ValidadeFinal { get; set; }
        public int[] Cursos { get; set; }
        public int[] TitulosAvulsos { get; set; }
        public int[] SituacoesAlunos { get; set; }
        public int[] TiposTitulos { get; set; }
    }
}
