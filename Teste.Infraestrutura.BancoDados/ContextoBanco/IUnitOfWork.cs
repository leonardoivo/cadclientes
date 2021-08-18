using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Infraestrutura.BancoDados.ContextoBanco
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
