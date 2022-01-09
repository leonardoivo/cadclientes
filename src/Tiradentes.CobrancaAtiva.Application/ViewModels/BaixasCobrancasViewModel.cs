using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels
{
    public class BaixasCobrancasViewModel
    {
        public DateTime DataBaixa { get; set; }
        public string UserName { get; set; }
        public int Etapa { get; set; }
        public int QuantidadeTipo1 { get; set; }
        public int QuantidadeTipo2 { get; set; }
        public int QuantidadeTipo3 { get; set; }
        public int QuantidadeErrosTipo1 { get; set; }
        public int QuantidadeErrosTipo2 { get; set; }
        public int QuantidadeErrosTipo3 { get; set; }
        public int TotalTipo1 { get; set; }
        public int TotalTipo2 { get; set; }
        public int TotalTipo3 { get; set; }
        public int TotalErrosTipo1 { get; set; }
        public int TotalErrosTipo2 { get; set; }
        public int TotalErrosTipo3 { get; set; }
    }
}