using System;
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
    public class ConflitoRepository : BaseRepository<ConflitoModel>, IConflitoRepository
    {
        public ConflitoRepository(CobrancaAtivaDbContext context) : base(context)
        { }

        public async Task<ModelPaginada<BuscaConflito>> Buscar(ConflitoQueryParam queryParams)
        {
            var query = DbSet
                            .Select(r => new BuscaConflito
                            {
                                Id = r.Id,
                                EmpresaParceiraTentativa = r.EmpresaParceiraTentativa,
                                EmpresaParceiraEnvio = r.EmpresaParceiraEnvio,
                                NomeAluno = r.NomeAluno,
                                CPF = r.CPF,
                                Matricula = r.Matricula,
                                NomeLote = r.NomeLote,
                                ConflitoDetalhes = r.ConflitoDetalhes
                            })
                            .AsQueryable();


            if (queryParams.EmpresasParceiraEnvio.Length > 0)
                query = query.Where(e => queryParams.EmpresasParceiraEnvio.Contains(e.Id));

            if (queryParams.EmpresasParceiraTentativa.Length > 0)
                query = query.Where(e => queryParams.EmpresasParceiraTentativa.Contains(e.Id));

            if (!string.IsNullOrEmpty(queryParams.CPF))
                query = query.Where(e => e.CPF.ToLower().Contains(queryParams.CPF.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.NomeLote))
                query = query.Where(e => e.NomeLote.ToLower().Contains(queryParams.NomeLote.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.NomeAluno))
                query = query.Where(e => e.NomeAluno.ToLower().Contains(queryParams.NomeAluno.ToLower()));

            query = query.Ordenar(queryParams.OrdenarPor, "Id", queryParams.SentidoOrdenacao == "desc");

            return await query.Paginar(queryParams.Pagina, queryParams.Limite);
        }

        public async Task<IEnumerable<ConflitoModel>> BuscarPorLote(string lote)
        {
            return await DbSet
                .Where(c => c.Lote == lote)
                .ToListAsync();
        }
    }
}