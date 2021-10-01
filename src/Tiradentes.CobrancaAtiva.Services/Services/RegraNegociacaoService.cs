using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
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

        public async Task<IList<RegraNegociacaoViewModel>> Buscar()
        {
            var list = await _repositorio.Buscar();

            return _map.Map<List<RegraNegociacaoViewModel>>(list);
        }
    }
}
