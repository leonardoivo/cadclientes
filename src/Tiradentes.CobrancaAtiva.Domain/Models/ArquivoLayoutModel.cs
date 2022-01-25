using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ArquivoLayoutModel : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DataHora { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string NomeUsuario {get;set;}
        public string Status { get; set; }
        public string Conteudo { get; set; }
        
    }
}
