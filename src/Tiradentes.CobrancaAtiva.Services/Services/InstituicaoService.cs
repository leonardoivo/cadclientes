using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class InstituicaoService : IInstituicaoService
    {
        protected readonly IInstituicaoRepository _repositorio;
        protected readonly IMapper _map;

        public InstituicaoService(IInstituicaoRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<IList<InstituicaoViewModel>> Buscar()
        {
            var instituicoes = await _repositorio.Buscar();

            return _map.Map<List<InstituicaoViewModel>>(instituicoes);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
