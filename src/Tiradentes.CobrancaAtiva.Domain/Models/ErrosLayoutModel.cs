using System;
namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ErrosLayoutModel : BaseModel
    {
        public DateTime DataHora { get; set; }
        public decimal Sequencia { get; set; }
        public string Descricao { get; set; }
    }
}
