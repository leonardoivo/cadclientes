using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _repositorio;

        public EnderecoService(IEnderecoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task BuscarPorCep()
        {
            await _repositorio.BuscarPorCep();
        }
    }
}
