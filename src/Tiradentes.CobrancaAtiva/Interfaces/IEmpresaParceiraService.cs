using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IEmpresaParceiraService
    {
        Task<IList<BuscaEmpresaParceiraViewModel>> Busca();
    }
}
