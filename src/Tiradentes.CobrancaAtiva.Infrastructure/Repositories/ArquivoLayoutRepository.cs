using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ArquivoLayoutRepository : BaseRepository<ArquivoLayoutModel>, IArquivoLayoutRepository
    {
        public ArquivoLayoutRepository(CobrancaAtivaDbContext context) : base(context)
        {

        }
    }
}
