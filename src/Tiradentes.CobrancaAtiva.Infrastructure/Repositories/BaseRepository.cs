using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : BaseModel, new()
    {
        protected readonly CobrancaAtivaDbContext Db;
        protected readonly DbSet<TModel> DbSet;

        public BaseRepository(CobrancaAtivaDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TModel>();
        }

        public virtual async Task<IList<TModel>> Buscar() =>
            await DbSet.AsNoTracking().ToListAsync();

        public virtual async Task<TModel> BuscarPorId(int id) =>
             await DbSet.FindAsync(id);

        public virtual async Task Criar(TModel model)
        {
            try
            {
                DbSet.Add(model);
                await SaveChanges();
            }
            finally
            {
                if (DbSet.Local.Count > 0)
                    DbSet.Local.Clear();
            }
        }

        public virtual async Task Alterar(TModel model)
        {

            try
            {
                DbSet.Update(model);
                await SaveChanges();
            }
            finally
            {
                if (DbSet.Local.Count > 0)
                    DbSet.Local.Clear();
            }
        }

        public virtual async Task Deletar(int id)
        {
            DbSet.Remove(new TModel { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
