using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoClientes.Domain.Models;

namespace GestaoClientes.Domain.Interfaces
{
    public interface IBaseRepository<TModel> : IDisposable where TModel: BaseModel
    {
        Task<IList<TModel>> Buscar();
        Task<TModel> BuscarPorId(int id);
        Task Criar(TModel model);
        Task Alterar(TModel model);
        Task Deletar(int id);
        Task AlterarVarios(IEnumerable<TModel> model);
    }
}
