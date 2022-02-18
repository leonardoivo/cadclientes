﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels.BaixaPagamento;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.DTO;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface ICobrancaService : IDisposable
    {
        IEnumerable<CriarRespostaViewModel> ExemplosRespostas();
        Task<CriarRespostaViewModel> Criar(CriarRespostaViewModel model, string cnpj);

        Task<RegularizarParcelasAcordoViewModel> RegularizarAcordoCobranca(RegularizarParcelasAcordoViewModel model);

        Task<IEnumerable<RespostaViewModel>> BuscarRepostaNaoIntegrada();

        Task BaixaManual(BaixaPagamentoParcelaManualViewModel viewModel);

        RespostaViewModel AlterarStatus(RespostaViewModel viewModel);
        Task<ModelPaginada<BaixaPagamento>> Listar(ConsultaBaixaPagamentoQueryParam queryParam);
        Task<IEnumerable<string>> ListarFiltrosMatricula(string matricula);
        Task<IEnumerable<string>> ListarFiltrosAcordo(string acordo);
        Task<IEnumerable<string>> ListarFiltroCpf(string cpf);
        Task<IEnumerable<string>> ListarFiltroNomeAluno(string nomeAluno);
    }
}