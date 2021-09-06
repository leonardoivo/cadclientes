using AutoMapper;
using System.Text.Json;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.Utils;
using Tiradentes.CobrancaAtiva.Application.Validations.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

using System.Linq;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class EmpresaParceiraService : BaseService, IEmpresaParceiraService
    {
        protected readonly IEmpresaParceiraRepository _repositorio;
        protected readonly IMapper _map;

        public EmpresaParceiraService(IEmpresaParceiraRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task VerificarCnpjJaCadastrado(string Cnpj)
        {
            var CnpjCadastrado = await _repositorio.VerificaCnpjJaCadastrado(Cnpj);
            
            if(CnpjCadastrado) throw CustomException.EntityNotFound(JsonSerializer.Serialize(new { erro = "CNPJ já cadastrado" }));
        }

        public async Task<EmpresaParceiraViewModel> BuscarPorId(int id)
        {
            var resultadoConsulta = await _repositorio.BuscarPorIdCompleto(id);
            return _map.Map<EmpresaParceiraViewModel>(resultadoConsulta);
        }

        public async Task<ViewModelPaginada<BuscaEmpresaParceiraViewModel>> Buscar(ConsultaEmpresaParceiraQueryParam queryParams)
        {
            var query = _map.Map<EmpresaParceiraQueryParam>(queryParams);
            var resultadoConsulta = await _repositorio.Buscar(query);
            return _map.Map<ViewModelPaginada<BuscaEmpresaParceiraViewModel>>(resultadoConsulta);
        }

        public async Task<EmpresaParceiraViewModel> Criar(EmpresaParceiraViewModel viewModel)
        {
            Validate(new CriarEmpresaParceiraValidation(), viewModel);

            viewModel.Id = 0;
            viewModel.Status = true;
            foreach(var contato in viewModel.Contatos)
            {
                contato.Id = 0;
            }

            var model = _map.Map<EmpresaParceiraModel>(viewModel);
            model.SetarEndereco(0, viewModel.CEP, viewModel.Estado, viewModel.Cidade,
                                viewModel.Logradouro, viewModel.Numero, 
                                viewModel.Complemento);

            await _repositorio.Criar(model);

            return _map.Map<EmpresaParceiraViewModel>(model);
        }

        public async Task<EmpresaParceiraViewModel> Atualizar(EmpresaParceiraViewModel viewModel)
        {
            var model = _map.Map<EmpresaParceiraModel>(viewModel);

            await _repositorio.Alterar(model);

            return _map.Map<EmpresaParceiraViewModel>(model);
        }

        public async Task Deletar(EmpresaParceiraViewModel viewModel)
        {
            var model = _map.Map<EmpresaParceiraModel>(viewModel);

            await _repositorio.Deletar(viewModel.Id);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
