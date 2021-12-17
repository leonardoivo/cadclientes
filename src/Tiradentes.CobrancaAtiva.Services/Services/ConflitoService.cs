using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;


namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito
{
    public class ConflitoViewModel: IConflitoService
    {
        public int Id { get; set; }
        public EmpresaParceira EmpresaParceiraTentativa { get; set; }

        public EmpresaParceira EmpresaParceiraEnvio { get; set; }

        public string NomeLote { get; set; }

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