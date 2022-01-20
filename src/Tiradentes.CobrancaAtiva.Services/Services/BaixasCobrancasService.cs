using AutoMapper;
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
            var baixaCobranca = await _baixasCobrancasRepository.BuscarPorDataBaixa(baixasCobrancas.DataBaixa);

            if (baixaCobranca == null)
                return;

            var model = _mapper.Map<BaixasCobrancasModel>(baixasCobrancas);

            await _baixasCobrancasRepository.Alterar(model);
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
            viewModel.ArquivoLayout = _arquivoLayoutService.BuscarPorDataHora(dataBaixa);

            return viewModel;
        }
    }
}
