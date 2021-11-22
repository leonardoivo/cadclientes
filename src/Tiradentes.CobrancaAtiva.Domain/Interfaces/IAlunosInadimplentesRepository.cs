using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Collections;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IAlunosInadimplentesRepository
    {
        Task<List<AlunosInadimplentesCollection>> GetAlunosInadimplentes();
    }
}
