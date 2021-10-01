using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class RegraNegociacaoService : IRegraNegociacaoService
    {
        private readonly IRegraNegociacaoRepository _repositorio;
        protected readonly IMapper _map;

        public RegraNegociacaoService(IRegraNegociacaoRepository repositorio, IMapper map)
        { 
            _map = map;
            _repositorio = repositorio;
        }

        public async Task<IList<BuscaRegraNegociacaoViewModel>> Buscar()
        {
            var list = await _repositorio.BuscarT();

            return _map.Map<List<BuscaRegraNegociacaoViewModel>>(list);
        }

        public async Task<RegraNegociacaoViewModel> Criar(CriarRegraNegociacaoViewModel viewModel)
        {
            var model = _map.Map<RegraNegociacaoModel>(viewModel);

            await _repositorio.Criar(model);

            return _map.Map<RegraNegociacaoViewModel>(model);
        }
    }
}
