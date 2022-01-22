using System;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class BaixasCobrancasModel : BaseModel
    {
        public DateTime DataBaixa { get; set; }
        public string UserName { get; set; }
        public int Etapa { get; set; } = 0;
        public int QuantidadeTipo1 { get; set; } = 0;
        public int QuantidadeTipo2 { get; set; } = 0;
        public int QuantidadeTipo3 { get; set; } = 0;
        public int QuantidadeErrosTipo1 { get; set; } = 0;
        public int QuantidadeErrosTipo2 { get; set; } = 0;
        public int QuantidadeErrosTipo3 { get; set; } = 0;
        public decimal TotalTipo1 { get; set; } = 0;
        public decimal TotalTipo2 { get; set; } = 0;
        public decimal TotalTipo3 { get; set; } = 0;
        public decimal TotalErrosTipo1 { get; set; } = 0;
        public decimal TotalErrosTipo2 { get; set; } = 0;
        public decimal TotalErrosTipo3 { get; set; } = 0;
    }
}
