using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class EmpresaParceiraRepository : BaseRepository<EmpresaParceiraModel>, IEmpresaParceiraRepository
    {
        public EmpresaParceiraRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}
