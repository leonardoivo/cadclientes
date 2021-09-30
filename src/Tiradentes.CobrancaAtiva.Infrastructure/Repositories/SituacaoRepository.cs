using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class SituacaoRepository : BaseRepository<SituacaoModel>, ISituacaoRepository
    {
        public SituacaoRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}
