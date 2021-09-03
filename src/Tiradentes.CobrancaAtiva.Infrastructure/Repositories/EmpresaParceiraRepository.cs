﻿using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.DTO;

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

        public async Task<ModelPaginada<EmpresaParceiraModel>> Buscar(EmpresaParceiraQueryParam queryParams)
        {
            var query = DbSet
                        .Include(e => e.Contatos)
                        .Include(e => e.Endereco)
                        .AsQueryable();

            if (!string.IsNullOrEmpty(queryParams.NomeFantasia))
                query = query.Where(e => e.NomeFantasia.Contains(queryParams.NomeFantasia));

            if (!string.IsNullOrEmpty(queryParams.CNPJ))
                query = query.Where(e => e.CNPJ.Contains(queryParams.CNPJ));

            if (!string.IsNullOrEmpty(queryParams.NomeFantasia))
                query = query.Where(e => e.NomeFantasia.Contains(queryParams.NomeFantasia));

            //if (!string.IsNullOrEmpty(queryParams.AditivoContrato))
            //    query = query.Where(e => e.AditivoContrato.Contains(queryParams.AditivoContrato));

            if (!string.IsNullOrEmpty(queryParams.Contato))
                query = query.Where(e => e.Contatos.Where(c => c.Contato.Contains(queryParams.Contato)).Any());

            if (!string.IsNullOrEmpty(queryParams.Estado))
                query = query.Where(e => e.Endereco.Estado.Contains(queryParams.Estado));

            if (!string.IsNullOrEmpty(queryParams.Cidade))
                query = query.Where(e => e.Endereco.Cidade.Contains(queryParams.Cidade));

            if (queryParams.Status.HasValue)
                query = query.Where(e => e.Status.Equals(queryParams.Status.Value));

            return await query.Paginar(queryParams.Pagina, queryParams.Limite);
        }
    }
}
