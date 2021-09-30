using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class SituacaoService : ISituacaoService
    {
        protected readonly ISituacaoRepository _repositorio;
        protected readonly IMapper _map;

        public SituacaoService(ISituacaoRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<IList<SituacaoViewModel>> Buscar()
        {
            var situacoes = await _repositorio.Buscar();

            return _map.Map<List<SituacaoViewModel>>(situacoes);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
