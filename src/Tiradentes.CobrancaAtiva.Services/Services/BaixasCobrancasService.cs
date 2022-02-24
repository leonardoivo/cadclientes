using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.BaixaPagamento;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
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

        public async Task<ViewModelPaginada<ConsultaBaixaPagamentoViewModel>> Buscar(
            ConsultaBaixaCobrancaQueryParam queryParams)
        {
            var query = _mapper.Map<BaixaCobrancaQueryParam>(queryParams);
            var resultadoConsulta = await _baixasCobrancasRepository.Buscar(query);
            var resultado = _mapper.Map<ViewModelPaginada<ConsultaBaixaPagamentoViewModel>>(resultadoConsulta);
            foreach (var item in resultado.Items)
            {
                item.QuantidadeErros = item.Items.Count(i => i.Erro.HasValue && i.Erro.Value != 0);
                item.QuantidadeTipo1 = item.Items.Count(i => i.Tipo == 1);
                item.QuantidadeTipo2 = item.Items.Count(i => i.Tipo == 2);
                item.QuantidadeTipo3 = item.Items.Count(i => i.Tipo == 3);
                item.ValorTipo1 = item.Items.Where(i => i.Tipo == 1).Sum(i => i.Valor);
                item.ValorTipo2 = item.Items.Where(i => i.Tipo == 2).Sum(i => i.Valor);
                item.ValorTipo3 = item.Items.Where(i => i.Tipo == 3).Sum(i => i.Valor);
            }

            return resultado;
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
            model.UserName = baixasCobrancas.UserName;

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