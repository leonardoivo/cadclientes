using System;
using System.Collections.Generic;
using Bogus;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;

namespace Tiradentes.CobrancaAtiva.Unit.Fakes
{
    public class EmpresaParceiraFake
    {
        public static Faker<EmpresaParceiraViewModel> GerarCriar { get; } =
            new Faker<EmpresaParceiraViewModel>("pt_BR")
            .RuleFor(cc => cc.Id, 123)
            .RuleFor(cc => cc.NomeFantasia, "Nome Fantasia")
            .RuleFor(cc => cc.RazaoSocial, "Razao Social")
            .RuleFor(cc => cc.Sigla, "RS")
            .RuleFor(cc => cc.CNPJ, "28.992.700/0001-29")
            .RuleFor(cc => cc.NumeroContrato, "NumeroContrato")
            .RuleFor(cc => cc.AditivoContrato, "AditivoContrato")
            .RuleFor(cc => cc.URL, "https://www.nomefantasia.com")
            .RuleFor(cc => cc.CEP, "42345234")
            .RuleFor(cc => cc.Estado, "SE")
            .RuleFor(cc => cc.Cidade, "Aracaju")
            .RuleFor(cc => cc.Logradouro, "Rua Pedro")
            .RuleFor(cc => cc.Numero, "7")
            .RuleFor(cc => cc.Complemento, "")
            .RuleFor(cc => cc.BancoId, 1213)
            .RuleFor(cc => cc.ContaCorrente, "42345234")
            .RuleFor(cc => cc.CodigoAgencia, "SE")
            .RuleFor(cc => cc.Convenio, "Aracaju")
            .RuleFor(cc => cc.Pix, "Pix")
            .RuleFor(cc => cc.Status, true)
            .RuleFor(cc => cc.TipoEnvioArquivo, "TipoEnvioArquivo")
            .RuleFor(cc => cc.IpEnvioArquivo, "IpEnvioArquivo")
            .RuleFor(cc => cc.PortaEnvioArquivo, 8080)
            .RuleFor(cc => cc.UsuarioEnvioArquivo , "UsuarioEnvioArquivo")
            .RuleFor(cc => cc.SenhaEnvioArquivo , "SenhaEnvioArquivo")
            .RuleFor(cc => cc.ChaveIntegracaoSap, "ChaveIntegracaoSap")
            .RuleFor(cc => cc.Contatos, new List<ContatoEmpresaParceiraViewModel>() { EmpresaParceiraFake.GerarCriarContato.Generate() });

        public static Faker<ContatoEmpresaParceiraViewModel> GerarCriarContato { get; } =
            new Faker<ContatoEmpresaParceiraViewModel>("pt_BR")
            .RuleFor(cc => cc.Contato, "Teste")
            .RuleFor(cc => cc.Email, "Razao Social")
            .RuleFor(cc => cc.Telefone, "RS");
    }
}
