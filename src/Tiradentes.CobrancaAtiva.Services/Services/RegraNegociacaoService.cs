using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.Validations.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class RegraNegociacaoService : BaseService, IRegraNegociacaoService
    {
        private readonly IRegraNegociacaoRepository _repositorio;
        protected readonly IMapper _map;

        public RegraNegociacaoService(IRegraNegociacaoRepository repositorio, IMapper map)
        { 
            _map = map;
            _repositorio = repositorio;
        }

        public async Task InativarRegrasNegociacao() 
        {
            var regrasParaInativar = await _repositorio.ListarRegrasParaInativar();

            foreach (var regraNegociacao in regrasParaInativar)
            {
                regraNegociacao.Status = false;
                await _repositorio.Alterar(regraNegociacao);
            }
        }

        public async Task AtivarRegrasNegociacao() 
        {
            var regrasParaInativar = await _repositorio.ListarRegrasParaAtivar();

            foreach (var regraNegociacao in regrasParaInativar)
            {
                regraNegociacao.Status = true;
                await _repositorio.Alterar(regraNegociacao);
            }
        }

        public async Task<ViewModelPaginada<BuscaRegraNegociacaoViewModel>> Buscar(ConsultaRegraNegociacaoQueryParam queryParam)
        {
            var regraQueryParam = _map.Map<RegraNegociacaoQueryParam>(queryParam);

            var list = await _repositorio.Buscar(regraQueryParam);

            return _map.Map<ViewModelPaginada<BuscaRegraNegociacaoViewModel>>(list);
        }

        public async Task<BuscaRegraNegociacaoViewModel> BuscarPorId(int id)
        {
            var list = await _repositorio.BuscarPorIdComRelacionamentos(id);

            return _map.Map<BuscaRegraNegociacaoViewModel>(list);
        }

        public async Task<RegraNegociacaoViewModel> Criar(CriarRegraNegociacaoViewModel viewModel)
        {
            Validate(new CriarRegraNegociacaoValidation(), viewModel);

            var model = _map.Map<RegraNegociacaoModel>(viewModel);

            await _repositorio.Criar(model);

            return _map.Map<RegraNegociacaoViewModel>(model);
        }

        public async Task<RegraNegociacaoViewModel> Alterar(AlterarRegraNegociacaoViewModel viewModel)
        {
            var modelBanco = await _repositorio.BuscarPorId(viewModel.Id);

            if (modelBanco == null) EntidadeNaoEncontrada("Regra de negociação não cadastrada.");

            var model = _map.Map<RegraNegociacaoModel>(viewModel);

            model.SetRegraNegociacaoCurso(modelBanco.RegraNegociacaoCurso);
            model.SetRegraNegociacaoTituloAvulso(modelBanco.RegraNegociacaoTituloAvulso);
            model.SetRegraNegociacaoSituacaoAluno(modelBanco.RegraNegociacaoSituacaoAluno);
            model.SetRegraRegraNegociacaoTipoTitulo(modelBanco.RegraNegociacaoTipoTitulo);

            await _repositorio.Alterar(model);

            return _map.Map<RegraNegociacaoViewModel>(model);
        }
    }
}
