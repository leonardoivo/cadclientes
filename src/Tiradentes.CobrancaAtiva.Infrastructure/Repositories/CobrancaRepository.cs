using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using System.Collections.Generic;
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

        public async Task<RespostasCollection> Criar(RespostasCollection model)
        {
            model.Id = System.Guid.NewGuid().ToString();

            await _repository.InsertOneAsync(model);
            return model;
        }

        public async Task<ICollection<RespostasCollection>> Listar(BaixaPagamentoQueryParam queryParam)
        {
            var query = _repository.AsQueryable();

            if(!string.IsNullOrEmpty(queryParam.Matricula))
                query.Where(c => c.Matricula.Equals(queryParam.Matricula));

            if(!string.IsNullOrEmpty(queryParam.Cpf))
                query.Where(c => c.CPF == queryParam.Cpf);

            if(!string.IsNullOrEmpty(queryParam.Acordo))
                query.Where(c => c.NumeroAcordo == queryParam.Acordo);

            if(!string.IsNullOrEmpty(queryParam.NomeAluno))
                query.Where(c => c.NomeAluno.Equals(queryParam.NomeAluno));

            return await query.ToListAsync();
        }

        public async Task<ICollection<RespostasCollection>> ListarFiltroPorMatricula(string matricula)
        {
            var query = _repository.AsQueryable();

            query = query.Where(b => b.Matricula.Contains(matricula));
            query = query.Take(25);

            return await query.ToListAsync();
        }      

        public async Task<ICollection<RespostasCollection>> ListarFiltroPorAluno(string aluno)
        {
            var query = _repository.AsQueryable();

            query = query.Where(b => b.NomeAluno.ToLower().Contains(aluno.ToLower()));
            query = query.Take(25);

            return await query.ToListAsync();
        } 

        public async Task<ICollection<RespostasCollection>> ListarFiltroPorCpf(string cpf)
        {
            var query = _repository.AsQueryable();

            query = query.Where(b => b.CPF.Contains(cpf));
            query = query.Take(25);

            return await query.ToListAsync();
        }      

        public async Task<ICollection<RespostasCollection>> ListarFiltroPorAcordo(string acordo)
        {
            var query = _repository.AsQueryable();

            query = query.Where(b => b.NumeroAcordo.Contains(acordo));
            query = query.Take(25);

            return await query.ToListAsync();
        }     
    }
}
