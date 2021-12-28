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
    public class LoteEnvioRepository : ILoteEnvioRepository
    {
        readonly IMongoCollection<LoteEnvioCollection> _repository;
        public LoteEnvioRepository(MongoContext context)
        {
            _repository = context.LoteEnvio;
        }

        public async Task<LoteEnvioCollection> GetLoteEnvio(string numeroLote)
        {
            return await (await _repository.FindAsync(lote => lote.Lote == new System.Guid(numeroLote))).FirstAsync();
        }
    }
}
