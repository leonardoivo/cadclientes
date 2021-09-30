using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class TipoPagamentoService : ITipoPagamentoService
    {
        protected readonly ITipoPagamentoRepository _repositorio;
        protected readonly IMapper _map;

        public TipoPagamentoService(ITipoPagamentoRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<IList<TipoPagamentoViewModel>> Buscar()
        {
            var tipoPagamentos = await _repositorio.Buscar();

            return _map.Map<List<TipoPagamentoViewModel>>(tipoPagamentos);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
