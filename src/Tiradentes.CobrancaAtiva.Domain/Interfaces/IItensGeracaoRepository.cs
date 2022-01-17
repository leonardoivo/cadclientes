using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IItensGeracaoRepository
    {
        Task Criar(ItensGeracaoModel model);
        Task CriarVarios(List<ItensGeracaoModel> models);
    }
}