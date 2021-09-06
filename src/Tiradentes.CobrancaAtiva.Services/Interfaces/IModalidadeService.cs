using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IModalidadeService : IDisposable
    {
        Task<IList<ModalidadeViewModel>> BuscarPorInstituicao(int instituicaoId);
    }
}
