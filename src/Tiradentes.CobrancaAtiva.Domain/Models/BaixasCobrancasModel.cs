using System;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class BaixasCobrancasModel : BaseModel
    {
        public DateTime DataBaixa { get; set; }
        public string? UserName { get; set; }
        public int Etapa { get; set; } = 0;
        public int QuantidadeTipo1 { get; set; } = 0;
        public int QuantidadeTipo2 { get; set; } = 0;
        public int QuantidadeTipo3 { get; set; } = 0;
        public int QuantidadeErrosTipo1 { get; set; } = 0;
        public int QuantidadeErrosTipo2 { get; set; } = 0;
        public int QuantidadeErrosTipo3 { get; set; } = 0;
        public int TotalTipo1 { get; set; } = 0;
        public int TotalTipo2 { get; set; } = 0;
        public int TotalTipo3 { get; set; } = 0;
        public int TotalErrosTipo1 { get; set; } = 0;
        public int TotalErrosTipo2 { get; set; } = 0;
        public int TotalErrosTipo3 { get; set; } = 0;
    }
}
