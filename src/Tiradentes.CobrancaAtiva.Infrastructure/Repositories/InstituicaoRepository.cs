using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class InstituicaoRepository : BaseRepository<InstituicaoModel>, IInstituicaoRepository
    {
        public InstituicaoRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}
