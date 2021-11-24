using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class AlunosInadimplentesRepository : IAlunosInadimplentesRepository
    {
        readonly IMongoCollection<AlunosInadimplentesCollection> _repository;
        public AlunosInadimplentesRepository(MongoContext context)
        {
            _repository = context.Dados;
        }

        public async Task<List<AlunosInadimplentesCollection>> GetAlunosInadimplentes()
        {
            return await (await _repository.FindAsync(_ => true)).ToListAsync();
        }
    }
}
