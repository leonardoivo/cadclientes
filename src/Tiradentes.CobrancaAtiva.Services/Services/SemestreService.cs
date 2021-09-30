using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Semestre;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class SemestreService : ISemestreService
    {
        protected readonly ISemestreRepository _repositorio;
        protected readonly IMapper _map;

        public SemestreService(ISemestreRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<IList<SemestreViewModel>> Buscar()
        {
            var tipoTitulos = await _repositorio.Buscar();

            return _map.Map<List<SemestreViewModel>>(tipoTitulos);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
