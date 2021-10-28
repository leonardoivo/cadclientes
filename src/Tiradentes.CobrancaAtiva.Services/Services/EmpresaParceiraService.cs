using AutoMapper;
using Renci.SshNet;
using System;
using System.IO;
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
        protected readonly IEmpresaParceiraRepository _repositorio;
        protected readonly IMapper _map;

        public EmpresaParceiraService(IEmpresaParceiraRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task VerificarCnpjJaCadastrado(string cnpj, int? id)
        {
            await ValidaCnpj(cnpj, id);
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

            await ValidaCnpj(viewModel.CNPJ);

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
            model.SetarContaBancaria(0, viewModel.ContaCorrente, viewModel.CodigoAgencia,
                                viewModel.Convenio, viewModel.Pix, viewModel.BancoId);

            await _repositorio.Criar(model);

            return _map.Map<EmpresaParceiraViewModel>(model);
        }

        public async Task<EmpresaParceiraViewModel> Atualizar(EmpresaParceiraViewModel viewModel)
        {
            Validate(new AtualizarEmpresaParceiraValidation(), viewModel);

            var modelNoBanco = await _repositorio.BuscarPorIdCompleto(viewModel.Id);

            if (modelNoBanco == null) EntidadeNaoEncontrada("Empresa não encontrada");

            await ValidaCnpj(viewModel.CNPJ, viewModel.Id);

            var model = _map.Map<EmpresaParceiraModel>(viewModel);
            
            model.SetarEndereco(modelNoBanco.Endereco.Id, viewModel.CEP, viewModel.Estado, viewModel.Cidade,
                    viewModel.Logradouro, viewModel.Numero, 
                    viewModel.Complemento);

            model.SetarContaBancaria(modelNoBanco.ContaBancaria.Id, viewModel.ContaCorrente, viewModel.CodigoAgencia,
                                viewModel.Convenio, viewModel.Pix, viewModel.BancoId);

            await _repositorio.Alterar(model);

            return _map.Map<EmpresaParceiraViewModel>(model);
        }

        public async Task Deletar(int id)
        {
            await _repositorio.Deletar(id);
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }

        private async Task ValidaCnpj(string cnpj, int? id = null)
        {
            var CnpjCadastrado = await _repositorio.VerificaCnpjJaCadastrado(cnpj, id);

            if (CnpjCadastrado) throw CustomException.BadRequest(JsonSerializer.Serialize(new { erro = "CNPJ já cadastrado" }));
        }

        public async void EnviarArquivoSftp(int id)
        {
            var empresaParceira = await _repositorio.BuscarPorId(id);

            using var client = new SftpClient(empresaParceira.IpSftp, empresaParceira.PortaSftp, empresaParceira.UsuarioSftp, empresaParceira.SenhaSftp);
            try
            {
                var filename = "arquivo-alunos-" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + ".csv";

                using StreamWriter file = new(filename);
                await file.WriteLineAsync("aluno,teste1,teste2");
                file.Close();

                client.Connect();
                using var s = File.OpenRead(filename);

                client.UploadFile(s, "/mnt/Dados/unit/unit_usr/" + filename);
            }
            catch(Renci.SshNet.Common.SshConnectionException sexc)
            {
                throw sexc;
            }
            catch(Renci.SshNet.Common.SftpPermissionDeniedException pexce)
            {
                throw pexce;
            }
            catch(Renci.SshNet.Common.SshException ssexc)
            {
                throw ssexc;
            }
            finally
            {
                client.Disconnect();
            }
        }
    }
}
