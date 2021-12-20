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
using Tiradentes.CobrancaAtiva.Application.QueryParams;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ConflitoDetalheService : BaseService, IConflitoDetalheService
    {
        protected readonly IConflitoDetalheRepository _repositorio;
        protected readonly IMapper _map;

        public ConflitoDetalheService(IConflitoDetalheRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<IList<ConflitoDetalheViewModel>> BuscarPorIdComRelacionamentos(int id)
        {

            var list = await _repositorio.BuscarPorIdComRelacionamentos(id);

            return _map.Map<List<ConflitoDetalheViewModel>>(list);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
