using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IHonorarioEmpresaParceiraService : IDisposable
    {
        Task<HonorarioEmpresaParceiraViewModel> Criar(CreateHonorarioEmpresaParceiraViewModel viewModel);
        Task<HonorarioEmpresaParceiraViewModel> Atualizar(CreateHonorarioEmpresaParceiraViewModel viewModel);
    }
}
