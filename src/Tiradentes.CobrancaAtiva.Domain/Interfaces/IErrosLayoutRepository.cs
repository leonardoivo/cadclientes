using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IErrosLayoutRepository : IBaseRepository<ErrosLayoutModel>
    {
        List<ErrosLayoutModel> BuscarPorDataHora(DateTime dataHora);
        void HabilitarAlteracaoErroLayout(bool status);
    }
}
