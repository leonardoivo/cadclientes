using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Enum;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IErrosLayoutRepository : IBaseRepository<ErrosLayoutModel>
    {
        List<ErrosLayoutModel> BuscarPorDataHora(DateTime dataHora);
        void HabilitarAlteracaoErroLayout(bool status);

        Task CriarErrosLayout(DateTime dataHora, string descricao);
    }
}
