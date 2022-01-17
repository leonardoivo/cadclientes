using AutoMapper;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
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


        public async Task<DateTime> SalvarLayoutArquivo(string status, RespostaViewModel arquivoResposta)
        {
            var layoutArquivo = new ArquivoLayoutModel()
            {
                DataHora = DateTime.Now,
                Conteudo = JsonSerializer.Serialize(arquivoResposta),
                Status = status
            };

            await _repository.Criar(layoutArquivo);

            return layoutArquivo.DataHora;
        }
        public async Task AtualizarStatusLayoutArquivo(DateTime dataHora, string status)
        {
            var model = _repository.BuscarPorDataHora(dataHora);

            model.Status = status;

            await _repository.Alterar(model);
        }

        public ArquivoLayoutViewModel BuscarPorDataHora(DateTime dataHora)
        {
            var model =  _repository.BuscarPorDataHora(dataHora);

            var viewModel = _mapper.Map<ArquivoLayoutViewModel>(model);
            viewModel.ErrosLayout = _erroLayoutService.BuscarPorDataHora(dataHora);

            return viewModel;
        }
    }
}
