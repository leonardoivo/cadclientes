using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class TipoPagamentoRepository : BaseRepository<TipoPagamentoModel>, ITipoPagamentoRepository
    {
        public TipoPagamentoRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}
