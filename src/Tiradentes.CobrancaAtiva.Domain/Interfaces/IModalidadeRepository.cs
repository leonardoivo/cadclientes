﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IModalidadeRepository : IBaseRepository<ModalidadeModel>
    {
        Task<IList<ModalidadeModel>> BuscarPorInstituicao(int instituicaoId);
        Task<ModalidadeModel> BuscarPorCodigo(string codigo);
    }
}
