using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class BancoMagisterRepository : BaseRepository<BancoMagisterModel>, IBancoMagisterRepository
    {
        public BancoMagisterRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}