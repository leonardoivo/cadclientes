using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ErrosLayoutModel : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Sequencia { get; set; }
        public DateTime DataHora { get; set; }
        public string Descricao { get; set; }        
    }
}
