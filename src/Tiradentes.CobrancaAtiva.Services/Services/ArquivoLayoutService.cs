using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<DateTime> SalvarLayoutArquivo(string status, string arquivoResposta, string cnpj, int iesId)
        {
            var layoutArquivo = new ArquivoLayoutModel()
            {
                Conteudo = arquivoResposta,
                Status = status,
                CnpjEmpresaCobranca = cnpj,
                InstituicaoEnsinoId = iesId
            };

            _repository.HabilitarAlteracaoArquivoLayout(true);
            
            await _repository.CriarArquivoLayout(layoutArquivo);

            _repository.HabilitarAlteracaoArquivoLayout(false);

            return layoutArquivo.DataHora;
        }
        public async Task AtualizarStatusLayoutArquivo(DateTime dataHora, string status)
        {
            var model = _repository.BuscarPorDataHora(dataHora).FirstOrDefault();

            if(model != null)
            {
                model.Status = status;

                _repository.HabilitarAlteracaoArquivoLayout(true);

                await _repository.Alterar(model);

                _repository.HabilitarAlteracaoArquivoLayout(false);
            }
        }

        public List<ArquivoLayoutViewModel> BuscarPorData(DateTime data)
        {
            var model = _repository.BuscarPorData(data);

            if (model.Count() == 0)
                return new List<ArquivoLayoutViewModel>();

            var listViewModel = _mapper.Map<List<ArquivoLayoutViewModel>>(model);

            foreach (var item in listViewModel)
            {

                item.ErrosLayout = _erroLayoutService.BuscarPorDataHora(data);
            }
            
            return listViewModel;
        }

        public ArquivoLayoutViewModel BuscarPorDataHora(DateTime dataHora)
        {
            var model =  _repository.BuscarPorDataHora(dataHora).FirstOrDefault();

            if (model == null)
                return null;

            var viewModel = _mapper.Map<ArquivoLayoutViewModel>(model);
            viewModel.ErrosLayout = _erroLayoutService.BuscarPorDataHora(dataHora);

            return viewModel;
        }

        public async Task<decimal?> RegistrarErro(DateTime dataBaixa, string conteudo, ErrosBaixaPagamento erro, string erroDescricao)
        {
            var model =  BuscarPorDataHora(dataBaixa);

            if(model != null)
            {
                await AtualizarStatusLayoutArquivo(dataBaixa, "E");            
            }

            return await _erroLayoutService.CriarErroLayoutService(dataBaixa, erro, erroDescricao);
        }

        public async Task AlterarConteudo(DateTime dataHora, object conteudo)
        {
            var arquivoViewModel = BuscarPorDataHora(dataHora);

            if (arquivoViewModel == null) return;

            arquivoViewModel.Conteudo = JsonSerializer.Serialize(conteudo);

            await _repository.Alterar(_mapper.Map<ArquivoLayoutModel>(arquivoViewModel));
        }
    }
}
