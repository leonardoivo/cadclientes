using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Infraestrutura.BancoDados.Repositorios.Interfaces
{
    public interface ITesteRepositorio : IBaseRepository<Teste.Dominio.Entidades.Teste>, IDisposable
    {
    }
}
