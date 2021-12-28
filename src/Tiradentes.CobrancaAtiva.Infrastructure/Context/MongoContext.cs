using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Context
{
    public class MongoContext
    {
        protected readonly IMongoDatabase _database;


        public MongoContext(IConfiguration config)
        {
            var mongoClient = new MongoClient("mongodb+srv://fief:6oUEyQE7sDKUXZcV@cluster0.pryqh.mongodb.net/mec?retryWrites=true&w=majority");
            _database = mongoClient.GetDatabase("mec");
        }

        public IMongoCollection<ApplicationErrorCollection> Log
        {
            get
            {
                return _database.GetCollection<ApplicationErrorCollection>("log-geral");
            }
        }

        public IMongoCollection<AlunosInadimplentesCollection> Dados
        {
            get
            {
                return _database.GetCollection<AlunosInadimplentesCollection>("alunos-inadimplentes");
            }
        }
    }
}