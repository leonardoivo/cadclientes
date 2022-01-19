using System;
using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ArquivoLayoutModel : BaseModel
    {
        public DateTime DataHora { get; set; }
        public string NomeUsuario {get;set;}
        public string Status { get; set; }
        public string Conteudo { get; set; }

        public List<ErrosLayoutModel> ErrosLayout { get; set; }
    }
}
