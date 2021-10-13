using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class CursoRepository : BaseRepository<CursoModel>, ICursoRepository
    {
        public CursoRepository(CobrancaAtivaDbContext context) : base(context)
        { }

        public async Task<IList<CursoModel>> BuscarPorInstituicaoModalidade(int instituicaoId, int modalidadeId)
        {
            return await DbSet
                .Include(c => c.Modalidade)
                .Include(c => c.Instituicao)
                .Where(c => c.InstituicaoId == instituicaoId)
                .Where(c => c.ModalidadeId == modalidadeId)
                .ToListAsync();
        }
    }
}
