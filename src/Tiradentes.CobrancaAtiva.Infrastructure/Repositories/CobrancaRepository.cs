﻿using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

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
            await _repository.InsertOneAsync(model);
            return model;
        }
    }
}
