using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.InstituicaoModalidadeRegra;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IInstituicaoModalidadeRegraService : IDisposable
    {
        Task<InstituicaoModalidadeRegraViewModel> Criar(CreateInstituicaoModalidadeRegraViewModel viewModel);
        Task<InstituicaoModalidadeRegraViewModel> Atualizar(CreateInstituicaoModalidadeRegraViewModel viewModel);
        Task<ViewModelPaginada<InstituicaoModalidadeRegraViewModel>> Buscar(ConsultaInstituicaoModalidadeRegraQueryParam queryParams);
    }
}
