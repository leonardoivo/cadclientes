using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class GeracaoCobrancasRepository : IGeracaoCobrancasRepository
    {
        private readonly CobrancaAtivaScfDbContext _context;
        public GeracaoCobrancasRepository(CobrancaAtivaScfDbContext context) 
        { 
            _context = context;
        }

        public virtual async Task Criar(GeracaoCobrancasModel model)
        {
            _context.GeracaoCobrancas.Add(model);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
