using System;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Services;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito
{
    public class ConflitoService : BaseService, IConflitoService
    {
        protected readonly IConflitoRepository _repositorio;
        protected readonly IMapper _map;

        public ConflitoService(IConflitoRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<IList<ConflitoViewModel>> Buscar()
        {
            var conflitos = await _repositorio.Buscar();

            return _map.Map<List<ConflitoViewModel>>(conflitos);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
