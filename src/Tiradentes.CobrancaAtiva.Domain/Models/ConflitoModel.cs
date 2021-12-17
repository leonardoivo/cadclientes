using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ConflitoModel : BaseModel
    {
        public ConflitoModel()
        { }

        public string NomeLote { get; set; }
        public int EmpresaParceiraTentativaId { get; set; }
        public int EmpresaParceiraEnvioId { get; set; }
        public int Matricula { get; set; }
        public string NomeAluno { get; set; }
        public string CPF { get; set; }
        public int ModalidadeId { get; set; }
        
        public string Parcela { get; set; }
        public decimal Valor { get; set; }
        //public bool Situacao { get; set; }
        public DateTime DataEnvio { get; set; }

        public EmpresaParceiraModel EmpresaParceiraTentativa { get; set; }
        public EmpresaParceiraModel EmpresaParceiraEnvio { get; set; }
        public ModalidadeModel Modalidade { get; set; }

    }
}
