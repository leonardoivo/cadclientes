﻿using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Enum;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IArquivoLayoutService
    {
        Task SalvarLayoutArquivo(DateTime dataBaixa, string conteudo, string status, ErrosBaixaPagamento erro, string erroDescricao);
        Task AtualizarStatusLayoutArquivo(DateTime dataHora, string status);
        ArquivoLayoutViewModel BuscarPorDataHora(DateTime dataHora);

        Task<decimal?> RegistrarErro(DateTime dataBaixa, string conteudo, ErrosBaixaPagamento erro, string erroDescricao);


    }
}
