using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class CursoRepository : BaseRepository<CursoModel>, ICursoRepository
    {
        public CursoRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}
