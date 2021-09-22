using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class HonorarioEmpresaParceiraRepository : BaseRepository<HonorarioEmpresaParceiraModel>, IHonorarioEmpresaParceiraRepository
    {
        public HonorarioEmpresaParceiraRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}
