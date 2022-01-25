using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito
{
    public class BuscaConflitoViewModel
    {
        public int Id { get; set; }
        public EmpresaParceiraModel EmpresaParceiraTentativa { get; set; }
        public EmpresaParceiraModel EmpresaParceiraEnvio { get; set; }
        public string NomeLote { get; set; }
        public int Matricula { get; set; }
        public string NomeAluno { get; set; }
        public string CPF { get; set; }
        

        public IEnumerable<ConflitoDetalheModel> ConflitoDetalhes { get; set; }
    }
}