using Tiradentes.CobrancaAtiva.Domain.Interfaces;
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

        public override async Task Alterar(EmpresaParceiraModel model)
        {
            var childs = await base.Db.ContatoEmpresaParceira
                .Include(c => c.Empresa)
                .Where(c => c.Empresa.Id == model.Id)
                .AsNoTracking()
                .ToListAsync();
                
            foreach (var item in childs) {
                if (!model.Contatos.Where(c => c.Id == item.Id).Any())
                    base.Db.Entry<ContatoEmpresaParceiraModel>(item).State = EntityState.Deleted;
            }

            await base.Alterar(model);
        }

        public async Task<bool> VerificaCnpjJaCadastrado(string cnpj, int? id) 
        {
            var query = DbSet.Where(e => e.CNPJ == cnpj);

            if (id.HasValue) query = query.Where(e => e.Id != id.Value);

            return await query.AnyAsync();
        }

        public async Task<EmpresaParceiraModel> BuscarPorIdCompleto(int id) =>
            await DbSet
                    .AsNoTracking()
                    .Include(e => e.Contatos)
                    .Include(e => e.Endereco)
                    .Include(e => e.ContaBancaria)
                    .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<ModelPaginada<EmpresaParceiraModel>> Buscar(EmpresaParceiraQueryParam queryParams)
        {
            var query = DbSet
                        .Include(e => e.Contatos)
                        .Include(e => e.Endereco)
                        .Include(e => e.ContaBancaria)
                        .AsQueryable();

            if (!string.IsNullOrEmpty(queryParams.NomeFantasia))
                query = query.Where(e => e.NomeFantasia.ToLower().Contains(queryParams.NomeFantasia.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.CNPJ))
                query = query.Where(e => e.CNPJ.Contains(queryParams.CNPJ));

            if (!string.IsNullOrEmpty(queryParams.NumeroContrato))
                query = query.Where(e => e.NumeroContrato.ToLower().Contains(queryParams.NumeroContrato.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.AditivoContrato))
                query = query.Where(e => e.AditivoContrato.ToLower().Contains(queryParams.AditivoContrato.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.Contato))
                query = query.Where(e => e.Contatos.Where(c => c.Contato.ToLower().Contains(queryParams.Contato.ToLower())).Any());

            if (!string.IsNullOrEmpty(queryParams.Estado))
                query = query.Where(e => e.Endereco.Estado.Equals(queryParams.Estado));

            if (!string.IsNullOrEmpty(queryParams.Cidade))
                query = query.Where(e => e.Endereco.Cidade.ToLower().Contains(queryParams.Cidade.ToLower()));

            if (queryParams.Status.HasValue)
                query = query.Where(e => e.Status.Equals(queryParams.Status.Value));

            query = query.Ordenar(queryParams.OrdenarPor, "NomeFantasia", queryParams.SentidoOrdenacao == "desc");

            return await query.Paginar(queryParams.Pagina, queryParams.Limite);
        }
    }
}
