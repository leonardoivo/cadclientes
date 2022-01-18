using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class HonorarioEmpresaParceiraRepository : BaseRepository<HonorarioEmpresaParceiraModel>, IHonorarioEmpresaParceiraRepository
    {
        public HonorarioEmpresaParceiraRepository(CobrancaAtivaDbContext context) : base(context)
        { }

        public async Task<ModelPaginada<HonorarioEmpresaParceiraModel>> Buscar(HonorarioEmpresaParceiraQueryParam queryParams)
        {
            var query = DbSet
                        .Include(h => h.Faixas)
                        .AsQueryable();

            if (queryParams.EmpresaParceiraId > 0)
                query = query.Where(e => e.EmpresaParceiraId == queryParams.EmpresaParceiraId);

            return await query.OrderBy(e => e.Id).Paginar(queryParams.Pagina, queryParams.Limite);
        }
    }
}
