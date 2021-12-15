using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BuscaConflito
    {
        public int Id { get; set; }
        public EmpresaParceiraModel EmpresaParceiraIn { get; set; }

        public EmpresaParceiraModel EmpresaParceiraOu { get; set; }

        public string NomeLote { get; set; }

        public int Matricula { get; set; }

        public string Nomealuno { get; set; }

        public string CPF { get; set; }

        public string ModalidadeEnsino { get; set; }

        public string ParcelaConflito { get; set; }

        public float ValorConflito { get; set; }

        public bool SituacaoConflito { get; set; }

        public DateTime DataEnvio { get; set; }


        //public IEnumerable<EmpresaParceiraModel> EmpresaParceirasIn { get; set; }
        //public IEnumerable<EmpresaParceiraModel> EmpresaParceirasOn { get; set; }

        //public IEnumerable<ModalidadeModel> Modalidade { get; set; }
        //public IEnumerable<SituacaoAlunoModel> SituacoesAlunos { get; set; }
        //public IEnumerable<TipoTituloModel> TiposTitulos { get; set; }


    }
}