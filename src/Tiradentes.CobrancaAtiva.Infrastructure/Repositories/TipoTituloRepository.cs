using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class TipoTituloRepository : BaseRepository<TipoTituloModel>, ITipoTituloRepository
    {
        public TipoTituloRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}
