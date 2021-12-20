using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BuscaConflito
    {
        public int Id { get; set; }
        public EmpresaParceiraModel EmpresaParceiraTentativa { get; set; }
        public EmpresaParceiraModel EmpresaParceiraEnvio { get; set; }
        public ModalidadeModel Modalidade { get; set; }
        public string NomeLote { get; set; }
        public int Matricula { get; set; }
        public string NomeAluno { get; set; }
        public string CPF { get; set; }
        public string Parcela { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}