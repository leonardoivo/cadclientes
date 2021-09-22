using AutoMapper;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.Validations.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class HonorarioEmpresaParceiraService : BaseService, IHonorarioEmpresaParceiraService
    {
        protected readonly IHonorarioEmpresaParceiraRepository _repositorio;
        protected readonly IMapper _map;

        public HonorarioEmpresaParceiraService(IHonorarioEmpresaParceiraRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task<HonorarioEmpresaParceiraViewModel> Criar(CreateHonorarioEmpresaParceiraViewModel viewModel)
        {
            Validate(new CriarHonorarioEmpresaParceiraValidation(), viewModel);
            
            var model = _map.Map<HonorarioEmpresaParceiraModel>(viewModel);

            await _repositorio.Criar(model);

            return _map.Map<HonorarioEmpresaParceiraViewModel>(model);
        }

        public async Task<HonorarioEmpresaParceiraViewModel> Atualizar(CreateHonorarioEmpresaParceiraViewModel viewModel)
        {
            Validate(new CriarHonorarioEmpresaParceiraValidation(), viewModel);
            
            var model = _map.Map<HonorarioEmpresaParceiraModel>(viewModel);

            await _repositorio.Alterar(model);

            return _map.Map<HonorarioEmpresaParceiraViewModel>(model);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
