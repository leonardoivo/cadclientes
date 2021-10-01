using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class SituacaoAlunoRepository : BaseRepository<SituacaoAlunoModel>, ISituacaoAlunoRepository
    {
        public SituacaoAlunoRepository(CobrancaAtivaDbContext context) : base(context)
        { }
    }
}
