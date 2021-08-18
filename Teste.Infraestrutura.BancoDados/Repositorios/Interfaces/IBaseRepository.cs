using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teste.Infraestrutura.BancoDados.Repositorios.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Find(int id);
        IQueryable<T> List();
        void Add(T item);
        void Remove(T item);
        void Edit(T item);
    }
}
