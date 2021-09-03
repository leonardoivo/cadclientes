﻿using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira
{
    public class EmpresaParceiraViewModel
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Sigla { get; set; }
        public string CNPJ { get; set; }
        public string NumeroContrato { get; set; }
        public string URL { get; set; }
        public bool Status { get; set; }

        public List<ContatoEmpresaParceiraViewModel> Contatos { get; set; }
        public EnderecoEmpresaParceiraViewModel Endereco { get; set; }
    }
}
