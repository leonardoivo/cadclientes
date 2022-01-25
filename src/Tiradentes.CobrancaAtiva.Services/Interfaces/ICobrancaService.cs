﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.DTO;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface ICobrancaService : IDisposable
    {
        Task<RespostaViewModel> Criar(RespostaViewModel model);

        Task<IEnumerable<RespostaViewModel>> BuscarRepostaNaoIntegrada();

        RespostaViewModel AlterarStatus(RespostaViewModel viewModel);
        Task<ModelPaginada<BaixaPagamento>> Listar(ConsultaBaixaPagamentoQueryParam queryParam);
        Task<IEnumerable<string>> ListarFiltrosMatricula(string matricula);
        Task<IEnumerable<string>> ListarFiltrosAcordo(string acordo);
        Task<IEnumerable<string>> ListarFiltroCpf(string cpf);
        Task<IEnumerable<string>> ListarFiltroNomeAluno(string nomeAluno);
    }
}
