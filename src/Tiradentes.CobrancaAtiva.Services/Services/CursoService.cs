using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class CursoService : ICursoService
    {
        protected readonly ICursoRepository _repositorio;
        protected readonly IMapper _map;

        public CursoService(ICursoRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<IList<CursoViewModel>> Buscar()
        {
            var tipoTitulos = await _repositorio.Buscar();

            return _map.Map<List<CursoViewModel>>(tipoTitulos);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
