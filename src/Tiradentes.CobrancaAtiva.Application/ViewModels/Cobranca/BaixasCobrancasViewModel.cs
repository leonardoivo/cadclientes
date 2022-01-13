using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca
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
        public decimal ValorTotalTipo1 { get; set; }
        public decimal ValorTotalTipo2 { get; set; }
        public decimal ValorTotalTipo3 { get; set; }
        public decimal ValorTotalErrosTipo1 { get; set; }
        public decimal ValorTotalErrosTipo2 { get; set; }
        public decimal ValorTotalErrosTipo3 { get; set; }
    }
}