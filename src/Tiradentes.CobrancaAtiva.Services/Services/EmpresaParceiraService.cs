using System;
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

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class EmpresaParceiraService : BaseService, IEmpresaParceiraService
    {
        private readonly ICriptografiaService _criptografiaService;
        private readonly IEmpresaParceiraRepository _repositorio;
        private readonly IMapper _map;

        public EmpresaParceiraService(
            IEmpresaParceiraRepository repositorio,
            IMapper map,
            ICriptografiaService criptografiaService
        )
        {
            _repositorio = repositorio;
            _map = map;
            _criptografiaService = criptografiaService;
        }

        public async Task VerificarCnpjJaCadastrado(string cnpj, int? id)
        {
            await ValidaCnpj(cnpj, id);
        }

        public async Task<EmpresaParceiraViewModel> BuscarPorId(int id)
        {
            var resultadoConsulta = await _repositorio.BuscarPorIdCompleto(id);
            var empresaParceira = _map.Map<EmpresaParceiraViewModel>(resultadoConsulta);
            if (empresaParceira != null)
                empresaParceira.SenhaApi = await _criptografiaService.Descriptografar(empresaParceira.SenhaApi);
            return empresaParceira;
        }

        public async Task<EmpresaParceiraViewModel> BuscarPorCnpj(string cnpj)
        {
            var resultadoConsulta = await _repositorio.BuscarPorCnpj(cnpj);
            var empresaParceira = _map.Map<EmpresaParceiraViewModel>(resultadoConsulta);
            empresaParceira.SenhaApi = await _criptografiaService.Descriptografar(empresaParceira.SenhaApi);
            return empresaParceira;
        }

        public async Task<ViewModelPaginada<BuscaEmpresaParceiraViewModel>> Buscar(
            ConsultaEmpresaParceiraQueryParam queryParams)
        {
            var query = _map.Map<EmpresaParceiraQueryParam>(queryParams);
            var resultadoConsulta = await _repositorio.Buscar(query);
            return _map.Map<ViewModelPaginada<BuscaEmpresaParceiraViewModel>>(resultadoConsulta);
        }

        public async Task<EmpresaParceiraViewModel> Criar(EmpresaParceiraViewModel viewModel)
        {
            Validate(new CriarEmpresaParceiraValidation(), viewModel);

            await ValidaCnpj(viewModel.CNPJ);

            viewModel.Id = 0;
            viewModel.Status = true;
            foreach (var contato in viewModel.Contatos)
            {
                contato.Id = 0;
            }

            if (viewModel.SenhaEnvioArquivo != null)
            {
                viewModel.SenhaEnvioArquivo = await _criptografiaService.Criptografar(viewModel.SenhaEnvioArquivo);
            }

            viewModel.SenhaApi = await _criptografiaService.Criptografar(RandomString.GeneratePassword(20, 5));

            var model = _map.Map<EmpresaParceiraModel>(viewModel);
            model.SetarEndereco(0, viewModel.CEP, viewModel.Estado, viewModel.Cidade,
                viewModel.Logradouro, viewModel.Numero,
                viewModel.Complemento);
            model.SetarContaBancaria(0, viewModel.ContaCorrente, viewModel.CodigoAgencia,
                viewModel.Convenio, viewModel.Pix, viewModel.BancoId);

            await _repositorio.Criar(model);

            var empresaParceira = _map.Map<EmpresaParceiraViewModel>(model);
            empresaParceira.SenhaApi = null;
            return empresaParceira;
        }

        public async Task<EmpresaParceiraViewModel> Atualizar(EmpresaParceiraViewModel viewModel)
        {
            Validate(new AtualizarEmpresaParceiraValidation(), viewModel);

            var modelNoBanco = await _repositorio.BuscarPorIdCompleto(viewModel.Id);

            if (modelNoBanco == null)
            {
                EntidadeNaoEncontrada("Empresa não encontrada");
            }

            await ValidaCnpj(viewModel.CNPJ, viewModel.Id);
            if (viewModel.SenhaEnvioArquivo != null)
            {
                viewModel.SenhaEnvioArquivo = await _criptografiaService.Criptografar(viewModel.SenhaEnvioArquivo);
            }

            if (string.IsNullOrEmpty(viewModel.SenhaApi))
                viewModel.SenhaApi = modelNoBanco.SenhaApi;
            else 
                viewModel.SenhaEnvioArquivo = await _criptografiaService.Criptografar(viewModel.SenhaEnvioArquivo);

            var model = _map.Map<EmpresaParceiraModel>(viewModel);
            model.SetarEndereco(modelNoBanco.Endereco.Id, viewModel.CEP, viewModel.Estado, viewModel.Cidade,
                viewModel.Logradouro, viewModel.Numero,
                viewModel.Complemento);
            model.SetarContaBancaria(modelNoBanco.ContaBancaria.Id, viewModel.ContaCorrente, viewModel.CodigoAgencia,
                viewModel.Convenio, viewModel.Pix, viewModel.BancoId);

            await _repositorio.Alterar(model);
            var empresaParceira = _map.Map<EmpresaParceiraViewModel>(model);
            empresaParceira.SenhaApi = null;
            return empresaParceira;
        }

        public async Task Deletar(int id)
        {
            await _repositorio.Deletar(id);
        }

        private async Task ValidaCnpj(string cnpj, int? id = null)
        {
            var CnpjCadastrado = await _repositorio.VerificaCnpjJaCadastrado(cnpj, id);

            if (CnpjCadastrado)
                throw CustomException.BadRequest(JsonSerializer.Serialize(new {erro = "CNPJ já cadastrado"}));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repositorio?.Dispose();
                _criptografiaService.Dispose();
            }
        }
    }
}