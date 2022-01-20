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
    public class CobrancaRepository : ICobrancaRepository
    {
        readonly IMongoCollection<RespostasCollection> _repository;

        public CobrancaRepository(MongoContext context)
        {
            _repository = context.Respostas;
        }

        public async Task<RespostasCollection> Criar(RespostasCollection model)
        {
            model.Id = System.Guid.NewGuid().ToString();

            await _repository.InsertOneAsync(model);
            return model;
        }

        public async Task<ICollection<RespostasCollection>> Listar(RespostasCollection model)
        {
            var query = _repository.AsQueryable();

            if(!string.IsNullOrEmpty(model.Matricula))
                query.Where(c => c.Matricula.Equals(model.Matricula));

            if(!string.IsNullOrEmpty(model.CPF))
                query.Where(c => c.CPF == model.CPF);

            if(!string.IsNullOrEmpty(model.NumeroAcordo))
                query.Where(c => c.NumeroAcordo == model.NumeroAcordo);

            if(!string.IsNullOrEmpty(model.NomeAluno))
                query.Where(c => c.NomeAluno.Equals(model.NomeAluno));

            return await query.ToListAsync();
        }
    }
}
