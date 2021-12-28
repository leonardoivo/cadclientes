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
    public class ApplicationErrorRepository : IApplicationErrorRepository
    {
        readonly IMongoCollection<ApplicationErrorCollection> _repository;

        public ApplicationErrorRepository(MongoContext context)
        {
            _repository = context.Log;
        }

        public async Task Insert(ApplicationErrorCollection model)
        {
            await _repository.InsertOneAsync(model);
        }
    }
}
