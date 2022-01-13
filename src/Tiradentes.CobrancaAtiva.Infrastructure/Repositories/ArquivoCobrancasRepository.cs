using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ArquivoCobrancasRepository : IArquivoCobrancasRepository
    {
        private readonly CobrancaAtivaScfDbContext _context;
        public ArquivoCobrancasRepository(CobrancaAtivaScfDbContext context) 
        { 
            _context = context;
        }

        public virtual async Task Criar(ArquivoCobrancasModel model)
        {
            await _context.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_arq_cob( true ); END;");
            _context.ArquivoCobrancas.Add(model);
            await SaveChanges();
            await _context.Database.ExecuteSqlRawAsync("BEGIN SCF.COBRANCAS_PKG.set_pode_alt_arq_cob( false ); END;");
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
