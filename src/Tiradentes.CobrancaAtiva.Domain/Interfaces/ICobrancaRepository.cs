using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Collections;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface ICobrancaRepository
    {
        Task<RespostasCollection> Criar(RespostasCollection model);
        Task<List<RespostasCollection>> Buscar(Expression<Func<RespostasCollection, bool>> query);
        Task<RespostasCollection> AlterarStatus(RespostasCollection model);
    }
}
