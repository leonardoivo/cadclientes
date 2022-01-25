using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Enums;

namespace Tiradentes.CobrancaAtiva.Domain.Collections
{
    public class AlunosInadimplentesCollection
    {
        public string Id { get; set; }
        public System.Guid Lote { get; set; }
        public string CodModalidadeEnsino { get; set; }
        public string DescricaoModalidadeEnsino { get; set; }
        public string TipoInadimplencia { get; set; }
        public string DescricaoTipoInadimplencia { get; set; }
        public string StatusAluno { get; set; }
        public string CodCurso { get; set; }
        public string NomeCurso { get; set; }
        public string IdtTipoTitulo { get; set; }
        public string TipoTituloAvulso { get; set; }
        public string DescricaoInadimplencia { get; set; }
        public string CpfAluno { get; set; }
        public string MatriculaAluno { get; set; }
        public string Periodo { get; set; }
        public string IdtAluno { get; set; }
        public string IdtDdp { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Uf { get; set; }
        public string DddResidencial { get; set; }
        public string TelefoneResidencial { get; set; }
        public string DddCelular { get; set; }
        public string TelefoneCelular { get; set; }
        public string Observacao { get; set; }
        public string Email { get; set; }
        public string ChaveInadimplencia { get; set; }
        public string NumeroParcela { get; set; }
        public System.DateTime DataVencimento { get; set; }
        public string ValorPagamento { get; set; }
        public string DescontoIncondicional { get; set; }
        public string IdtCampus { get; set; }
        public string DescricaoCampus { get; set; }
        public string Mae { get; set; }
        public string Pai { get; set; }
        public string NumCi { get; set; }
        public StatusNegociacao StatusNegociacao { get; set; }
        public List<HashCode> RespostasId { get; set; }
    }
}
