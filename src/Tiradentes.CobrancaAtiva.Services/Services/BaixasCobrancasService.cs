﻿using AutoMapper;
using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class BaixasCobrancasService : IBaixasCobrancasService
    {
        readonly IBaixasCobrancasRepository _baixasCobrancasRepository;
        readonly IArquivoLayoutService _arquivoLayoutService;
        readonly IMapper _mapper;
        public BaixasCobrancasService(IBaixasCobrancasRepository baixasCobrancasRepository,
                                      IArquivoLayoutService arquivoLayoutService,
                                      IMapper mapper)
        {
            _baixasCobrancasRepository = baixasCobrancasRepository;
            _arquivoLayoutService = arquivoLayoutService;
            _mapper = mapper;
        }
        public async Task AtualizarBaixasCobrancas(BaixasCobrancasViewModel baixasCobrancas)
        {
            //Update do doc n faz sentido
            var model = await _baixasCobrancasRepository.BuscarPorDataBaixa(baixasCobrancas.DataBaixa);

            if (model == null)
                return;            

            model.Etapa = baixasCobrancas.Etapa;
            model.QuantidadeErrosTipo1 = baixasCobrancas.QuantidadeErrosTipo1;
            model.QuantidadeErrosTipo2 = baixasCobrancas.QuantidadeErrosTipo2;
            model.QuantidadeErrosTipo3 = baixasCobrancas.QuantidadeErrosTipo3;
            model.QuantidadeTipo1 = baixasCobrancas.QuantidadeTipo1;
            model.QuantidadeTipo2 = baixasCobrancas.QuantidadeTipo2;
            model.QuantidadeTipo3 = baixasCobrancas.QuantidadeTipo3;
            model.TotalErrosTipo1 = baixasCobrancas.ValorTotalErrosTipo1;
            model.TotalErrosTipo2 = baixasCobrancas.ValorTotalErrosTipo2;
            model.TotalErrosTipo3 = baixasCobrancas.ValorTotalErrosTipo3;
            model.TotalTipo1 = baixasCobrancas.ValorTotalTipo1;
            model.TotalTipo2 = baixasCobrancas.ValorTotalTipo2;
            model.TotalTipo3 = baixasCobrancas.ValorTotalTipo3;            

            _baixasCobrancasRepository.HabilitarAlteracaoBaixaCobranca(true);

            await _baixasCobrancasRepository.Alterar(model);

            _baixasCobrancasRepository.HabilitarAlteracaoBaixaCobranca(false);
        }

        public async Task CriarBaixasCobrancas(DateTime dataBaixa)
        {
            _baixasCobrancasRepository.HabilitarAlteracaoBaixaCobranca(true);

            var model = new BaixasCobrancasModel()
            {
                DataBaixa = dataBaixa.Date,
                Etapa = 0

            };

            await _baixasCobrancasRepository.Criar(model);

            _baixasCobrancasRepository.HabilitarAlteracaoBaixaCobranca(false);
        }

        public async Task<BaixasCobrancasViewModel> Buscar(DateTime dataBaixa)
        {
            var baixaCobranca = await _baixasCobrancasRepository.BuscarPorDataBaixa(dataBaixa);

            if (baixaCobranca == null)
                return null;

            var viewModel = _mapper.Map<BaixasCobrancasViewModel>(baixaCobranca);
            viewModel.ArquivoLayout = _arquivoLayoutService.BuscarPorData(dataBaixa);

            return viewModel;
        }
    }
}
