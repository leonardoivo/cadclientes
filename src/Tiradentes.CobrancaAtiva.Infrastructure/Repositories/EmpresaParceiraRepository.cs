using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class EmpresaParceiraRepository : BaseRepository<EmpresaParceiraModel>, IEmpresaParceiraRepository
    {
        public EmpresaParceiraRepository(CobrancaAtivaDbContext context) : base(context)
        { }

        public async Task<bool> VerificaCnpjJaCadastrado(string Cnpj) 
        {
            return await base.DbSet.FirstOrDefaultAsync(e => e.CNPJ == Cnpj) != null;
        }
    }
}
