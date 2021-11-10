using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.Validations.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ParametroEnvioService : BaseService, IParametroEnvioService
    {
        private readonly IParametroEnvioRepository _repositorio;
        protected readonly IMapper _map;

        public ParametroEnvioService(IParametroEnvioRepository repositorio, IMapper map)
        { 
            _map = map;
            _repositorio = repositorio;
        }

        public async Task<ViewModelPaginada<BuscaParametroEnvioViewModel>> Buscar(ConsultaParametroEnvioQueryParam queryParam)
        {
            var regraQueryParam = _map.Map<ParametroEnvioQueryParam>(queryParam);

            var list = await _repositorio.Buscar(regraQueryParam);

            return _map.Map<ViewModelPaginada<BuscaParametroEnvioViewModel>>(list);
        }

        public async Task<BuscaParametroEnvioViewModel> BuscarPorId(int id)
        {
            var list = await _repositorio.BuscarPorIdComRelacionamentos(id);

            return _map.Map<BuscaParametroEnvioViewModel>(list);
        }

        public async Task<ParametroEnvioViewModel> Criar(CriarParametroEnvioViewModel viewModel)
        {
            //Validate(new CriarRegraNegociacaoValidation(), viewModel);

            var model = _map.Map<ParametroEnvioModel>(viewModel);

            await _repositorio.Criar(model);

            return _map.Map<ParametroEnvioViewModel>(model);
        }

        public async Task<ParametroEnvioViewModel> Alterar(AlterarParametroEnvioViewModel viewModel)
        {
            var modelBanco = await _repositorio.BuscarPorId(viewModel.Id);

            if (modelBanco == null)
            {
                EntidadeNaoEncontrada("Parametro envio não cadastrado.");
                return null;
            }

            var model = _map.Map<ParametroEnvioModel>(viewModel);

            model.SetParametroEnvioCurso(modelBanco.ParametroEnvioCurso);
            model.SetParametroEnvioSituacaoAluno(modelBanco.ParametroEnvioSituacaoAluno);
            model.SetParametroEnvioTituloAvulso(modelBanco.ParametroEnvioTituloAvulso);
            model.SetParametroEnvioTipoTitulo(modelBanco.ParametroEnvioTipoTitulo);

            await _repositorio.Alterar(model);

            return _map.Map<ParametroEnvioViewModel>(model);
        }
    }
}
