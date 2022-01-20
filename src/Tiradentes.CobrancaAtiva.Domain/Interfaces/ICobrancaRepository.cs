using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Collections;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface ICobrancaRepository
    {
        Task<RespostasCollection> Criar(RespostasCollection model);

        Task<ICollection<RespostasCollection>> Listar(RespostasCollection model);
    }
}
