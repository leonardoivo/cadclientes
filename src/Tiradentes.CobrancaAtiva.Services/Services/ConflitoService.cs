using System;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Services;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito;
using Tiradentes.CobrancaAtiva.Application.ViewModels;

namespace Tiradentes.CobrancaAtiva.Services.Services
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

        public async Task<ViewModelPaginada<ConflitoViewModel>> Buscar(ConflitoQueryParam queryParam)
        {
            var regraQueryParam = _map.Map<ConflitoQueryParam>(queryParam);

            var list = await _repositorio.Buscar(regraQueryParam);

            return _map.Map<ViewModelPaginada<ConflitoViewModel>>(list);
        }
        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
