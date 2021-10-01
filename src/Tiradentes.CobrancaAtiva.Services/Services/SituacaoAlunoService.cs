using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.SituacaoAluno;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class SituacaoAlunoService : ISituacaoAlunoService
    {
        protected readonly ISituacaoAlunoRepository _repositorio;
        protected readonly IMapper _map;

        public SituacaoAlunoService(ISituacaoAlunoRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<IList<SituacaoAlunoViewModel>> Buscar()
        {
            var situacoes = await _repositorio.Buscar();

            return _map.Map<List<SituacaoAlunoViewModel>>(situacoes);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
