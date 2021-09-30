using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class SemestreRepository : BaseRepository<SemestreModel>, ISemestreRepository
    {
        public SemestreRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}
