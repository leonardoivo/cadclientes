using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IArquivoLayoutRepository : IBaseRepository<ArquivoLayoutModel>
    {
        List<ArquivoLayoutModel> BuscarPorDataHora(DateTime dataHora);
        ArquivoLayoutModel BuscarLayoutSucessoPorData(DateTime dataHora);
        void HabilitarAlteracaoArquivoLayout(bool status);

        Task CriarArquivoLayout(ArquivoLayoutModel model);
    }
}
