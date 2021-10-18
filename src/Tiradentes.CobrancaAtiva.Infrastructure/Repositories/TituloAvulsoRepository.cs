using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class TituloAvulsoRepository : BaseRepository<TituloAvulsoModel>, ITituloAvulsoRepository
    {
        public TituloAvulsoRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}
