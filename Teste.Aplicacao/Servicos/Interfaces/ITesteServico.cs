using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Aplicacao.Servicos.Interfaces
{
    public interface ITesteServico : IBaseService<Teste.Dominio.Entidades.Teste>, IDisposable
    {
    }
}
