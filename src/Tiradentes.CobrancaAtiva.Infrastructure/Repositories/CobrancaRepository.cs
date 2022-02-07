using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using MongoDB.Driver.Linq;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class CobrancaRepository : ICobrancaRepository
    {
        readonly IMongoCollection<RespostasCollection> _repository;

        public CobrancaRepository(MongoContext context)
        {
            _repository = context.Respostas;
        }

        public async Task<List<RespostasCollection>> Buscar(Expression<Func<RespostasCollection, bool>> query)
        {
            return await _repository.Find<RespostasCollection>(query).ToListAsync();
        }

        public async Task<RespostasCollection> Criar(RespostasCollection model)
        {
            model.Id = System.Guid.NewGuid().ToString();

            await _repository.InsertOneAsync(model);
            
            return model;
        }

        public async Task<RespostasCollection> AlterarStatus(RespostasCollection model)
        {
            await _repository.UpdateOneAsync(Builders<RespostasCollection>.Filter.Eq("_id", model.Id), Builders<RespostasCollection>.Update.Set("Integrado", model.Integrado));
            
            return model;
        }

        public async Task<ICollection<RespostasCollection>> Listar(BaixaPagamentoQueryParam queryParam)
        {
            var query = _repository.AsQueryable();

            if(!string.IsNullOrEmpty(queryParam.Matricula))
                query = query.Where(c => c.Matricula.Equals(queryParam.Matricula));

            if(!string.IsNullOrEmpty(queryParam.Cpf))
                query = query.Where(c => c.CPF.ToString() == queryParam.Cpf);

            if(!string.IsNullOrEmpty(queryParam.Acordo))
                query = query.Where(c => c.NumeroAcordo.ToString() == queryParam.Acordo);

            if(!string.IsNullOrEmpty(queryParam.NomeAluno))
                query = query.Where(c => c.NomeAluno.Equals(queryParam.NomeAluno));

            return await query.ToListAsync();
        }

        public async Task<ICollection<RespostasCollection>> ListarFiltroPorMatricula(string matricula)
        {
            var query = _repository.AsQueryable();

            if(!string.IsNullOrEmpty(matricula))
                query = query.Where(b => b.Matricula.ToString().Contains(matricula));

            query = query.Take(25);

            return await query.ToListAsync();
        }      

        public async Task<ICollection<RespostasCollection>> ListarFiltroPorAluno(string aluno)
        {
            var query = _repository.AsQueryable();

            if(!string.IsNullOrEmpty(aluno))
                query = query.Where(b => b.NomeAluno.ToLower().Contains(aluno.ToLower()));
            query = query.Take(25);

            return await query.ToListAsync();
        } 

        public async Task<ICollection<RespostasCollection>> ListarFiltroPorCpf(string cpf)
        {
            var query = _repository.AsQueryable();

            if(!string.IsNullOrEmpty(cpf))
                query = query.Where(b => b.CPF.ToString().Contains(cpf));

            query = query.Take(25);

            return await query.ToListAsync();
        }      

        public async Task<ICollection<RespostasCollection>> ListarFiltroPorAcordo(string acordo)
        {
            var query = _repository.AsQueryable();

            if(!string.IsNullOrEmpty(acordo))
                query = query.Where(b => b.NumeroAcordo.ToString().Contains(acordo));
            query = query.Take(25);

            return await query.ToListAsync();
        }     
    }
}
