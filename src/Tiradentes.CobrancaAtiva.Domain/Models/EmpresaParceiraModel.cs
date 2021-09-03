﻿using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class EmpresaParceiraModel : BaseModel
    {
        public EmpresaParceiraModel() { }

        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Sigla { get; set; }
        public string CNPJ { get; set; }
        public string NumeroContrato { get; set; }
        public string URL { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public bool Status { get; set; }

        public ICollection<ContatoEmpresaParceiraModel> Contatos { get; set; }
    }
}
