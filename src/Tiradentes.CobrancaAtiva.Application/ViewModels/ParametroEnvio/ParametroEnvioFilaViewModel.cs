using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio
{
    public class ParametroEnvioFilaViewModel
    {        
        public int IdParametroEnvio { get; set; }
        public Int64 Lote { get; set;}
        public string StatusEnvio { get; set; }
        public int Instituicao { get; set; }
        public string Modalidade { get; set; }
        public int EmpresaParceira { get; set; }
        public DateTime InadimplenciaInicial { get; set; }
        public DateTime InadimplenciaFinal { get; set; }
        public DateTime ValidadeInicial { get; set; }
        public DateTime ValidadeFinal { get; set; }
        public string[] Cursos { get; set; }
        public string[] SituacoesAluno { get; set; }
        public string[] TitulosAvulsos { get; set; }
        public string[] TiposTitulos { get; set; }
    }
}
