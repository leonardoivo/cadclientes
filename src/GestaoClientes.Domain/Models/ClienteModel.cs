using GestaoClientes.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoClientes.Domain.Models
{
    public class ClienteModel:BaseModel
    {
        public int IdCliente { get; set; }

        public string Nome { get; set; }

        public Porte Porte { get; set; }

    }   
  
}
