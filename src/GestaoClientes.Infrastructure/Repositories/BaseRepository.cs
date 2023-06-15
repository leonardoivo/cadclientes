using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Domain.Models;
using GestaoClientes.Infrastructure.Context;

namespace GestaoClientes.Infrastructure.Repositories
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : BaseModel, new()
    {
        protected readonly GestaoClientesDbContext Db;
        protected readonly DbSet<TModel> DbSet;

        public BaseRepository(GestaoClientesDbContext context)
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

            DbSet.Add(model);
            await SaveChanges();

        }

        public virtual async Task Alterar(TModel model)
        {

            DbSet.Update(model);
            await SaveChanges();

        }

        public virtual async Task Deletar(int id)
        {
            DbSet.Remove(new TModel { Id = id });
            await SaveChanges();
        }

        public virtual async Task AlterarVarios(IEnumerable<TModel> model)
        {
            DbSet.UpdateRange(model);
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
