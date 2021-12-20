using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ConflitoDetalheRepository : BaseRepository<ConflitoDetalheModel>, IConflitoDetalheRepository
    {
        public ConflitoDetalheRepository(CobrancaAtivaDbContext context) : base(context)
        { }

        public async Task<IList<ConflitoDetalheModel>> BuscarPorIdConflitoComRelacionamentos(int idConflito)
        {
            return await DbSet
                .Include(c => c.Modalidade)
                .Where(c => c.ConflitoId == idConflito)
                .ToListAsync();

        }
    }
}
