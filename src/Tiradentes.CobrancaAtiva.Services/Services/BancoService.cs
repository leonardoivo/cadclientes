using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Banco;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class BancoService : IBancoService
    {
        protected readonly IBancoRepository _repositorio;
        protected readonly IMapper _map;

        public BancoService(IBancoRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<IList<BancoViewModel>> Buscar()
        {
            var bancos = await _repositorio.Buscar();

            return _map.Map<List<BancoViewModel>>(bancos);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
