using AutoMapper;
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
        readonly IErrosLayoutRepository _erroLayoutRepository;
        readonly IErroLayoutService _erroLayoutService;
        readonly IMapper _mapper;
        public ArquivoLayoutService(IArquivoLayoutRepository repository,                                    
                                    IErrosLayoutRepository erroLayoutRepository,
                                    IErroLayoutService erroLayoutService,
                                    IMapper mapper)
        {
            _repository = repository;
            _erroLayoutRepository = erroLayoutRepository;
            _erroLayoutService = erroLayoutService;
            _mapper = mapper;
        }

        private async Task AtualizarLayoutArquivo(DateTime dataBaixa, string conteudo, string status, ErrosBaixaPagamento erro, string erroDescricao)
        {
            var model = _repository.BuscarPorDataHora(dataBaixa);

            model.Status = status;
            model.Conteudo = conteudo;

            if (model.ErrosLayout == null)
                model.ErrosLayout = new System.Collections.Generic.List<ErrosLayoutModel>();

            model.ErrosLayout.Add(new ErrosLayoutModel() {
                DataHora = dataBaixa,
                Descricao = string.IsNullOrEmpty(erroDescricao) ? Application.Utils.Utils.GetDescricaoEnum(erro) : Application.Utils.Utils.GetDescricaoEnum(erro) + " => " + erroDescricao
            }                 
            );

            try
            {
                _repository.HabilitarAlteracaoArquivoLayout(true);
                _erroLayoutRepository.HabilitarAlteracaoErroLayout(true);

                await _repository.Alterar(model);

            }
            finally
            {

                _erroLayoutRepository.HabilitarAlteracaoErroLayout(false);
                _repository.HabilitarAlteracaoArquivoLayout(false);
            }

        }

        public async Task SalvarLayoutArquivo(DateTime dataBaixa, string conteudo, string status, ErrosBaixaPagamento erro, string erroDescricao)
        {

            var layoutArquivo = new ArquivoLayoutModel()
            {
                DataHora = dataBaixa,
                Conteudo = conteudo,
                Status = status,
                ErrosLayout = new System.Collections.Generic.List<ErrosLayoutModel>() { new ErrosLayoutModel() { 
                    DataHora = dataBaixa,
                    Descricao =  string.IsNullOrEmpty(erroDescricao) ? Application.Utils.Utils.GetDescricaoEnum(erro) : Application.Utils.Utils.GetDescricaoEnum(erro) + " => " + erroDescricao
                } }
            };

            try
            {
                _repository.HabilitarAlteracaoArquivoLayout(true);
                _erroLayoutRepository.HabilitarAlteracaoErroLayout(true);

                await _repository.Criar(layoutArquivo);

            }
            finally
            {

                _erroLayoutRepository.HabilitarAlteracaoErroLayout(false);
                _repository.HabilitarAlteracaoArquivoLayout(false);
            }

        }
        public async Task AtualizarStatusLayoutArquivo(DateTime dataHora, string status)
        {
            _repository.HabilitarAlteracaoArquivoLayout(true);
            
            var model = _repository.BuscarPorDataHora(dataHora);

            model.Status = status;

            await _repository.Alterar(model);

            _repository.HabilitarAlteracaoArquivoLayout(false);            
        }

        public ArquivoLayoutViewModel BuscarPorDataHora(DateTime dataHora)
        {
            var model =  _repository.BuscarPorDataHora(dataHora);

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


                await SalvarLayoutArquivo(dataBaixa, conteudo, "E", erro, erroDescricao);
                
            }
            else
            {
                _repository.HabilitarAlteracaoArquivoLayout(true);

                await AtualizarLayoutArquivo(dataBaixa, conteudo, "E", erro, erroDescricao);

                _repository.HabilitarAlteracaoArquivoLayout(false);
            }

            return  -99999;            
        }
    }
}
