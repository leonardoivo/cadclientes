using System;
using System.Collections;
using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BuscaBaixaPagamentoDto
    {
        public DateTime DataBaixa { get; set; }
        public string NomeEmpresaParceira { get; set; }
        public string NomeInstituicaoEnsino { get; set; }
        public IEnumerable<ItensBaixaDto> Items { get; set; }

    }
}