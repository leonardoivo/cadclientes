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



            if (!string.IsNullOrEmpty(queryParams.ModalidadeEnsino))
                query = query.Where(e => e.ModalidadeEnsino.ToLower().Contains(queryParams.ModalidadeEnsino.ToLower()));
            if (!string.IsNullOrEmpty(queryParams.CPF))
                query = query.Where(e => e.CPF.ToLower().Contains(queryParams.CPF.ToLower()));

            if (queryParams.DataEnvio.HasValue && queryParams.DataEnvio != null)
                query = query.Where(e => e.DataEnvio == queryParams.DataEnvio);

            if (queryParams.Matricula == 0 && queryParams.Matricula != null)
                query = query.Where(e => e.Matricula == queryParams.Matricula);

            if (!string.IsNullOrEmpty(queryParams.NomeLote))
                query = query.Where(e => e.NomeLote.ToLower().Contains(queryParams.NomeLote.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.ParcelaConflito))
                query = query.Where(e => e.ParcelaConflito.ToLower().Contains(queryParams.ParcelaConflito.ToLower()));
            if (queryParams.ValorConflito == 0 && queryParams.ValorConflito != null)
                query = query.Where(e => e.ValorConflito == queryParams.ValorConflito);
            if (!string.IsNullOrEmpty(queryParams.Nomealuno))
                query = query.Where(e => e.Nomealuno.ToLower().Contains(queryParams.Nomealuno.ToLower()));

            query = query.Ordenar(queryParams.OrdenarPor, "DataEnvio", queryParams.SentidoOrdenacao == "desc");

            return await query.Paginar(queryParams.Pagina, queryParams.Limite);
        }
    }
}
