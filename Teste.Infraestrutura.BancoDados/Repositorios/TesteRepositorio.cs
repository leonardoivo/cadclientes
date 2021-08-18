using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Teste.Infraestrutura.BancoDados.ContextoBanco;
using Teste.Infraestrutura.BancoDados.Repositorios.Interfaces;

namespace Teste.Infraestrutura.BancoDados.Repositorios
{
    public class TesteRepositorio : BaseRepository<Teste.Dominio.Entidades.Teste>, ITesteRepositorio
    {
        public TesteRepositorio(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
