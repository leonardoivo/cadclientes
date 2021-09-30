using AutoMapper;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.Validations.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.InstituicaoModalidadeRegra;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class InstituicaoModalideRegraService : BaseService, IInstituicaoModalidadeRegraService
    {
        protected readonly IInstituicaoModalidadeRegraRepository _repositorio;
        protected readonly IMapper _map;

        public InstituicaoModalideRegraService(IInstituicaoModalidadeRegraRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<InstituicaoModalidadeRegraViewModel> Criar(CreateInstituicaoModalidadeRegraViewModel viewModel)
        {
            //Validate(new CriarHonorarioEmpresaParceiraValidation(), viewModel);
            
            var model = _map.Map<InstituicaoModalidadeRegraModel>(viewModel);

            await _repositorio.Criar(model);

            return _map.Map<InstituicaoModalidadeRegraViewModel>(model);
        }

        public async Task<InstituicaoModalidadeRegraViewModel> Atualizar(CreateInstituicaoModalidadeRegraViewModel viewModel)
        {
            //Validate(new CriarHonorarioEmpresaParceiraValidation(), viewModel);
            
            var model = _map.Map<InstituicaoModalidadeRegraModel>(viewModel);

            await _repositorio.Alterar(model);

            return _map.Map<InstituicaoModalidadeRegraViewModel>(model);
        }

        public async Task<ViewModelPaginada<InstituicaoModalidadeRegraViewModel>> Buscar(ConsultaInstituicaoModalidadeRegraQueryParam queryParams)
        {
            var query = _map.Map<InstituicaoModalidadeRegraQueryParam>(queryParams);
            var resultadoConsulta = await _repositorio.Buscar(query);
            return _map.Map<ViewModelPaginada<InstituicaoModalidadeRegraViewModel>>(resultadoConsulta);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
