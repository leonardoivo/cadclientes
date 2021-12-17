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
                                Modalidade = r.Modalidade,
                                NomeAluno = r.NomeAluno,
                                CPF = r.CPF,
                                Matricula = r.Matricula,
                                NomeLote = r.NomeLote,
                                Parcela = r.Parcela,
                                Valor = r.Valor,
                                DataEnvio = r.DataEnvio
                            })
                            .AsQueryable();


            if (queryParams.EmpresasParceiraEnvio.Length > 0)
                query = query.Where(e => queryParams.EmpresasParceiraEnvio.Contains(e.Id));
            if (queryParams.EmpresasParceiraTentativa.Length > 0)
                query = query.Where(e => queryParams.EmpresasParceiraTentativa.Contains(e.Id));



            if (queryParams.Modalidades.Length > 0)
                query = query.Where(e => queryParams.Modalidades.Contains(e.Modalidade.Id));
            if (!string.IsNullOrEmpty(queryParams.CPF))
                query = query.Where(e => e.CPF.ToLower().Contains(queryParams.CPF.ToLower()));

            if (queryParams.DataEnvio.HasValue && queryParams.DataEnvio != null)
                query = query.Where(e => e.DataEnvio == queryParams.DataEnvio);

            if (queryParams.Matriculas.Length > 0)
                query = query.Where(e => queryParams.Matriculas.Contains(e.Matricula));

            if (!string.IsNullOrEmpty(queryParams.NomeLote))
                query = query.Where(e => e.NomeLote.ToLower().Contains(queryParams.NomeLote.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.Parcela))
                query = query.Where(e => e.Parcela.ToLower().Contains(queryParams.Parcela.ToLower()));
            if (queryParams.Valor == 0 && queryParams.Valor != null)
                query = query.Where(e => e.Valor == queryParams.Valor);

            if (!string.IsNullOrEmpty(queryParams.NomeAluno))
                query = query.Where(e => e.NomeAluno.ToLower().Contains(queryParams.NomeAluno.ToLower()));

            query = query.Ordenar(queryParams.OrdenarPor, "DataEnvio", queryParams.SentidoOrdenacao == "desc");

            return await query.Paginar(queryParams.Pagina, queryParams.Limite);
        }
    }
}
