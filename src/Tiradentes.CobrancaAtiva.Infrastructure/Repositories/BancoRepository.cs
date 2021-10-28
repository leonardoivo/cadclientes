using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class BancoRepository : BaseRepository<BancoModel>, IBancoRepository
    {
        public BancoRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}
