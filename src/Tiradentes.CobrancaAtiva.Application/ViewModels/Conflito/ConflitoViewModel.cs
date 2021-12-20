using System;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito
{
    public class ConflitoViewModel
    {
        public int Id { get; set; }

        public string NomeLote { get; set; }
        public int Matricula { get; set; }
        public string NomeAluno { get; set; }
        public string CPF { get; set; }

        public string Parcela { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataEnvio { get; set; }

        public EmpresaParceiraViewModel EmpresaParceiraTentativa { get; set; }
        public EmpresaParceiraViewModel EmpresaParceiraEnvio { get; set; }
        public ModalidadeViewModel Modalidade { get; set; }
    }
}