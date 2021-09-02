using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly CobrancaAtivaDbContext _context;
        public EnderecoRepository(CobrancaAtivaDbContext context)
        {
            _context = context;
        }

        public async Task BuscarPorCep()
        {
            var teste = await _context.Database.ExecuteSqlRawAsync("P_OBTER_DADOS_CEP @p0", parameters: new[] { "03050281030" });
        }
    }
}
