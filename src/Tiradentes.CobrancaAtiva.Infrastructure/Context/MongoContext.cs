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
            var conn = config["MongoDb:Url"];
            var database = config["MongoDb:DataBase"];

            var mongoClient = new MongoClient(conn);
            _database = mongoClient.GetDatabase(database);
        }

        public IMongoCollection<ApplicationErrorCollection> Log
        {
            get
            {
                return _database.GetCollection<ApplicationErrorCollection>("log-geral");
            }
        }

        public IMongoCollection<LoteEnvioCollection> LoteEnvio
        {
            get 
            {
                return _database.GetCollection<LoteEnvioCollection>("lote-envio");
            }
        }

        public IMongoCollection<AlunosInadimplentesCollection> Dados
        {
            get
            {
                return _database.GetCollection<AlunosInadimplentesCollection>("alunos-inadimplentes");
            }
        }

        public IMongoCollection<RespostasCollection> Respostas
        {
            get
            {
                return _database.GetCollection<RespostasCollection>("respostas-cobrancas-lucas");
            }
        }
    }
}