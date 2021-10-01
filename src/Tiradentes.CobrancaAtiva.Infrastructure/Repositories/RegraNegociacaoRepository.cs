using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class RegraNegociacaoRepository : BaseRepository<RegraNegociacaoModel>, IRegraNegociacaoRepository
    {
        public RegraNegociacaoRepository(CobrancaAtivaDbContext context) : base(context)
        { }

        public async Task<IList<BuscaRegraNegociacao>> BuscarT()
        {
            return await  DbSet
                            .Select(r  => new BuscaRegraNegociacao {
                                Instituicao = r.Instituicao.Instituicao,
                                Modedalidade = r.Modalidade.Modalidade,
                                PercentJurosMulta = r.PercentJurosMulta,
                                PercentValor = r.PercentValor,
                                Status = r.Status,
                                Validade = r.Validade,
                                Cursos = r.RegraNegociacaoCurso.Select(x => x.Curso)
                            })
                            .ToListAsync();
        }
    }
}
