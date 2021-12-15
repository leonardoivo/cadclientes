using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
  public  class ConflitoModel:BaseModel
    {

        public ConflitoModel()
        { }

        public EmpresaParceiraModel EmpresaParceiraIn { get; set; }

        public EmpresaParceiraModel EmpresaParceiraOu { get; set; }

        public string  NomeLote { get; set; }

        public int Matricula { get; set; }

        public string Nomealuno { get; set; }

        public string CPF { get; set; }

        public string ModalidadeEnsino { get; set; }

        public string ParcelaConflito { get; set; }

        public float ValorConflito { get; set; }

        public bool SituacaoConflito { get; set; }

        public DateTime DataEnvio { get; set; }



    }
}
