using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Enums;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ConflitoDetalheModel 
    {
        public ConflitoDetalheModel()
        { }

        public int ConflitoId { get; set; }
        public int ModalidadeId { get; set; }
        public string Parcela { get; set; }
        public decimal Valor { get; set; }
        public TipoConflito TipoConflito { get; set; }
        public DateTime DataEnvio { get; set; }

        public ConflitoModel Conflito { get; set; }
        public ModalidadeModel Modalidade { get; set; }
    }
}
