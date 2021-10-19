using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.TituloAvulso;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class TituloAvulsoService : ITituloAvulsoService
    {
        protected readonly ITituloAvulsoRepository _repositorio;
        protected readonly IMapper _map;

        public TituloAvulsoService(ITituloAvulsoRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<IList<TituloAvulsoViewModel>> Buscar()
        {
            var tipoTitulos = await _repositorio.Buscar();

            return _map.Map<List<TituloAvulsoViewModel>>(tipoTitulos);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
