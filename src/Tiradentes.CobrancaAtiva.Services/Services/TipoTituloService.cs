using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class TipoTituloService : ITipoTituloService
    {
        protected readonly ITipoTituloRepository _repositorio;
        protected readonly IMapper _map;

        public TipoTituloService(ITipoTituloRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<IList<TipoTituloViewModel>> Buscar()
        {
            var tipoTitulos = await _repositorio.Buscar();

            return _map.Map<List<TipoTituloViewModel>>(tipoTitulos);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
