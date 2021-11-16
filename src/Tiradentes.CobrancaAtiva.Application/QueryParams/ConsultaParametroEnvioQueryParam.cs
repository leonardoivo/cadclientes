using System;

namespace Tiradentes.CobrancaAtiva.Application.QueryParams
{
    public class ConsultaParametroEnvioQueryParam : BasePaginacaoQueryParam
    {
        public int EmpresaId { get; set; }
        public int DiaEnvio { get; set; }
        public bool? Status { get; set; }
        public DateTime? InadimplenciaInicial { get; set; }
        public DateTime? InadimplenciaFinal { get; set; }
        public DateTime? ValidadeInicial { get; set; }
        public DateTime? ValidadeFinal { get; set; }
        public int[] Instituicoes { get; set; }
        public int[] Modalidades { get; set; }
        public int[] Cursos { get; set; }
        public int[] TitulosAvulsos { get; set; }
        public int[] SituacoesAlunos { get; set; }
        public int[] TiposTitulos { get; set; }
    }
}
