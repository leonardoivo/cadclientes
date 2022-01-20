﻿using AutoMapper;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Enum;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ArquivoLayoutService : IArquivoLayoutService
    {
        readonly IArquivoLayoutRepository _repository;
        readonly IErroLayoutService _erroLayoutService;
        readonly IMapper _mapper;
        public ArquivoLayoutService(IArquivoLayoutRepository repository,
                                    IErroLayoutService erroLayoutService,
                                    IMapper mapper)
        {
            _repository = repository;
            _erroLayoutService = erroLayoutService;
            _mapper = mapper;
        }

        public async Task<DateTime> SalvarLayoutArquivo(string status, string arquivoResposta)
        {
            var layoutArquivo = new ArquivoLayoutModel()
            {
                //Conteudo = arquivoResposta,
                Conteudo = "Conteudo indisponivel",
                Status = status
            };

            _repository.HabilitarAlteracaoArquivoLayout(true);
            
            await _repository.CriarArquivoLayout(layoutArquivo);

            _repository.HabilitarAlteracaoArquivoLayout(false);

            return layoutArquivo.DataHora;
        }
        public async Task AtualizarStatusLayoutArquivo(DateTime dataHora, string status)
        {
            var model = _repository.BuscarLayoutSucessoPorData(dataHora);

            if(model != null)
            {
                model.Status = status;

                _repository.HabilitarAlteracaoArquivoLayout(true);

                await _repository.Alterar(model);

                _repository.HabilitarAlteracaoArquivoLayout(false);
            }
        }

        public ArquivoLayoutViewModel BuscarPorDataHora(DateTime dataHora)
        {
            var model =  _repository.BuscarLayoutSucessoPorData(dataHora);

            if (model == null)
                return null;

            var viewModel = _mapper.Map<ArquivoLayoutViewModel>(model);
            viewModel.ErrosLayout = _erroLayoutService.BuscarPorDataHora(dataHora);

            return viewModel;
        }

        public async Task<decimal?> RegistrarErro(DateTime dataBaixa, string conteudo, ErrosBaixaPagamento erro, string erroDescricao)
        {
            var arquivoLayout = BuscarPorDataHora(dataBaixa);

            if(arquivoLayout == null)
            {
                await SalvarLayoutArquivo("E", erroDescricao);
            }
            else
            {
                if (arquivoLayout.Status != "E")
                    await AtualizarStatusLayoutArquivo(dataBaixa, "E");            
            }

            return await _erroLayoutService.CriarErroLayoutService(dataBaixa, erro, erroDescricao);
        }
    }
}
