using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ModalidadeService : IModalidadeService
    {
        protected readonly IModalidadeRepository _repositorio;
        protected readonly IMapper _map;

        public ModalidadeService(IModalidadeRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<ModalidadeViewModel> BuscarPorCodigo(string modalidade)
        {
            return _map.Map<ModalidadeViewModel>(await _repositorio.BuscarPorCodigo(modalidade));
        }

        public async Task<IList<ModalidadeViewModel>> BuscarPorInstituicao(int instituicaoId)
        {
            var modalidades = await _repositorio.BuscarPorInstituicao(instituicaoId);

            return _map.Map<List<ModalidadeViewModel>>(modalidades);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
