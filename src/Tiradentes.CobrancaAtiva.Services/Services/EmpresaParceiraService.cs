using AutoMapper;
using Renci.SshNet;
using System;
using System.Collections.Generic;
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

            if (modelNoBanco == null) 
            {
                EntidadeNaoEncontrada("Empresa não encontrada");
                return null;
            }

            await ValidaCnpj(viewModel.CNPJ, viewModel.Id);

            var model = _map.Map<EmpresaParceiraModel>(viewModel);
            
            model.SetarEndereco(modelNoBanco.Endereco.Id, viewModel.CEP, viewModel.Estado, viewModel.Cidade,
                    viewModel.Logradouro, viewModel.Numero, 
                    viewModel.Complemento);

            model.SetarContaBancaria(modelNoBanco.ContaBancaria.Id, viewModel.ContaCorrente, viewModel.CodigoAgencia,
                                viewModel.Convenio, viewModel.Pix, viewModel.BancoId);

            model.IpSftp = modelNoBanco.IpSftp;
            model.SenhaSftp = modelNoBanco.SenhaSftp;
            model.PortaSftp = modelNoBanco.PortaSftp;
            model.UsuarioSftp = modelNoBanco.UsuarioSftp;

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

        public async Task EnviarArquivoSftp(int id)
        {
            var empresaParceira = await _repositorio.BuscarPorId(id);

            using var client = new SftpClient(empresaParceira.IpSftp, empresaParceira.PortaSftp.Value, empresaParceira.UsuarioSftp, empresaParceira.SenhaSftp);
            try
            {
                
                client.Connect();

                foreach(var filename in await GerarArquivoCsv(empresaParceira))
                {
                    using var s = File.OpenRead(filename);

                    client.UploadFile(s, "/mnt/Dados/unit/unit_usr/" + filename);

                    s.Close();
                }
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

        public async Task<List<string>> GerarArquivoCsv(EmpresaParceiraModel empresaParceira)
        {
            var arquivosGerados = new List<string>();

            var limiteLinhas = 2;

            var filenameTemplate = "{0}_{1}_{2}_{3}_{4}_PARTE{5}de{6}.csv";
            var cabecalhoCsv = "\"CNPJ EMPRESA COBRANÇA\",\"MODALIDADE DE ENSINO\",\"DESCRIÇÃO DA MODALIDADE DE ENSINO\",\"IDENTIFICADOR INSTITUIÇÃO DE ENSINO\",\"DESCRIÇÃO INSTIUIÇÃO DE ENSINO\",\"IDENTIFICADOR CURSO\",\"DESCRIÇÃO CURSO\",\"TIPO TITULO\",\"DESCRIÇÃO TIPO TÍTULO\",\"TIPO TITULO AVULSO\",\"DESCRICAO INADIMPLENCIA\",\"SITUACAO ALUNO\",\"CPF ALUNO\",\"MATRICULA\",\"PERIODO\",\"IDENTIFICADOR DO ALUNO\",\"IDENTIFICADOR DA PESSOA\",\"NOME\",\"ENDERECO\",\"BAIRRO\",\"CIDADE\",\"CEP\",\"UF\",\"DDD RES\",\"TELEFONE RES\",\"DDD CELULAR\",\"TELEFONE CELULAR\",\"EMAIL DO ALUNO\",\"NUMERO CONTRATO\",\"CONDIÇÃO DE NEGOCIAÇÃO\",\"DESCONTO INCONDICIONAL\",\"VALIDADE DA NEGOCIAÇÃO\",\"NUMERO DA PARCELA\",\"DATA VENCIMENTO\",\"VALOR PARCELA\",OBSERVACAO\",\"CODIGO DA CAMPUS IES\",\"NOME DA CAMPUS IES\",\"FILIACAO - MAE\",\"FILIACAO - PAINUMERO DO RG\"";

            var dados = new string[10] {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
            };

            var totalLinhas = dados.Length;
            var quantidadeArquivos = (int) (totalLinhas / limiteLinhas);

            for(var indexArquivoAtual = 1; indexArquivoAtual <= quantidadeArquivos; indexArquivoAtual++)
            {
                var filename = string.Format(filenameTemplate, "1", "1", "04-11-2021", "01-11-2021", "04-11-2021", indexArquivoAtual.ToString(), quantidadeArquivos.ToString());
                using StreamWriter file = new(filename);
                await file.WriteLineAsync(cabecalhoCsv);
                for(var indexDadosInicial = (indexArquivoAtual - 1) * limiteLinhas; indexDadosInicial <= (indexArquivoAtual * limiteLinhas) - 1; indexDadosInicial++)
                {
                    await file.WriteLineAsync(dados[indexDadosInicial]);
                }
                file.Close();
                arquivosGerados.Add(filename);
            }

            return arquivosGerados;
        }
    }
}
