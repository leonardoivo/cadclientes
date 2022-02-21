using AutoMapper;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio;
using Tiradentes.CobrancaAtiva.Application.WebApi;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ParametroEnvioService : BaseService, IParametroEnvioService
    {
        protected readonly EncryptationApi _encryptationApi;
        private readonly ConnectionFactory _factory;
        private readonly RabbitMQConfig _rabbitMQConfig;
        private readonly IParametroEnvioRepository _repositorio;
        private readonly IEmpresaParceiraRepository _repositorioEmpresaParceira;
        private readonly IGeracaoCobrancasRepository _geracaoCobrancaRepositorio;
        private readonly IItensGeracaoRepository _itensGeracaoRepository;
        private readonly IArquivoCobrancasRepository _arquivosGeracaoRepository;
        protected readonly IAlunosInadimplentesRepository _repositorioAlunosInadimplentes;
        protected readonly ILoteEnvioRepository _repositorioLoteEnvio;
        protected readonly IConflitoRepository _repositorioConflito;
        protected readonly IMapper _map;

        public ParametroEnvioService(
            IParametroEnvioRepository repositorio,
            IEmpresaParceiraRepository repositorioEmpresaParceira,
            IGeracaoCobrancasRepository geracaoCobrancaRepositorio,
            IItensGeracaoRepository itensGeracaoRepository,
            IArquivoCobrancasRepository arquivoCobrancasRepository,
            IAlunosInadimplentesRepository repositorioAlunosInadimplentes,
            ILoteEnvioRepository repositorioLoteEnvio,
            IConflitoRepository repositoryConflito,
            IMapper map,
            IOptions<RabbitMQConfig> rabbitMQConfig,
            IOptions<EncryptationConfig> encryptationConfig
        )
        {
            _map = map;
            _repositorio = repositorio;
            _repositorioEmpresaParceira = repositorioEmpresaParceira;
            _geracaoCobrancaRepositorio = geracaoCobrancaRepositorio;
            _itensGeracaoRepository = itensGeracaoRepository;
            _arquivosGeracaoRepository = arquivoCobrancasRepository;
            _repositorioAlunosInadimplentes = repositorioAlunosInadimplentes;
            _repositorioLoteEnvio = repositorioLoteEnvio;
            _repositorioConflito = repositoryConflito;
            _rabbitMQConfig = rabbitMQConfig.Value;

            _factory = new ConnectionFactory
            {
                HostName = _rabbitMQConfig.HostName,
                VirtualHost = _rabbitMQConfig.VirtualHost,
                UserName = _rabbitMQConfig.UserName,
                Password = _rabbitMQConfig.Password
            };
            _encryptationApi = new EncryptationApi(encryptationConfig.Value);
        }

        public async Task<ViewModelPaginada<BuscaParametroEnvioViewModel>> Buscar(
            ConsultaParametroEnvioQueryParam queryParam)
        {
            var regraQueryParam = _map.Map<ParametroEnvioQueryParam>(queryParam);

            var list = await _repositorio.Buscar(regraQueryParam);

            return _map.Map<ViewModelPaginada<BuscaParametroEnvioViewModel>>(list);
        }

        public async Task<BuscaParametroEnvioViewModel> BuscarPorId(int id)
        {
            var list = await _repositorio.BuscarPorIdComRelacionamentos(id);

            return _map.Map<BuscaParametroEnvioViewModel>(list);
        }

        public async Task EnviarParametroParaConsumer(int id)
        {
            var parametroEnvio = await _repositorio.BuscarPorIdComRelacionamentos(id);

            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: _rabbitMQConfig.Queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var viewModels = new ParametroEnvioFilaViewModel();
                    viewModels.IdParametroEnvio = parametroEnvio.Id;
                    viewModels.StatusEnvio = "AGUARDANDO_DADOS";
                    viewModels.Instituicao = parametroEnvio.Instituicao.Id;
                    viewModels.Modalidade = parametroEnvio.Modalidade.CodigoMagister;
                    viewModels.InadimplenciaInicial = parametroEnvio.InadimplenciaInicial;
                    viewModels.InadimplenciaFinal = parametroEnvio.InadimplenciaFinal;
                    viewModels.EmpresaParceira = parametroEnvio.EmpresaParceira.Id;
                    viewModels.ValidadeInicial = parametroEnvio.ValidadeInicial;
                    viewModels.ValidadeFinal = parametroEnvio.ValidadeFinal;
                    viewModels.Cursos = parametroEnvio.Cursos.Select(x => x.CodigoMagister.ToString()).ToArray();
                    viewModels.SituacoesAluno = parametroEnvio.SituacoesAlunos.Select(x => x.CodigoMagister.ToString())
                        .ToArray();
                    viewModels.TiposTitulos =
                        parametroEnvio.TiposTitulos.Select(x => x.CodigoMagister.ToString()).ToArray();
                    viewModels.TitulosAvulsos =
                        parametroEnvio.TitulosAvulsos.Select(x => x.CodigoGT.ToString()).ToArray();

                    var stringfiedMessage = JsonSerializer.Serialize(viewModels);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: _rabbitMQConfig.Queue,
                        basicProperties: null,
                        body: bytesMessage);
                }
            }
        }

        public async Task<ParametroEnvioViewModel> Criar(CriarParametroEnvioViewModel viewModel)
        {
            //Validate(new CriarRegraNegociacaoValidation(), viewModel);

            var model = _map.Map<ParametroEnvioModel>(viewModel);

            await _repositorio.Criar(model);

            return _map.Map<ParametroEnvioViewModel>(model);
        }

        public async Task<ParametroEnvioViewModel> Alterar(AlterarParametroEnvioViewModel viewModel)
        {
            var modelBanco = await _repositorio.BuscarPorId(viewModel.Id);

            if (modelBanco == null)
            {
                EntidadeNaoEncontrada("Parametro envio não cadastrado.");
                return null;
            }

            var model = _map.Map<ParametroEnvioModel>(viewModel);

            model.SetParametroEnvioCurso(modelBanco.ParametroEnvioCurso);
            model.SetParametroEnvioSituacaoAluno(modelBanco.ParametroEnvioSituacaoAluno);
            model.SetParametroEnvioTituloAvulso(modelBanco.ParametroEnvioTituloAvulso);
            model.SetParametroEnvioTipoTitulo(modelBanco.ParametroEnvioTipoTitulo);

            await _repositorio.Alterar(model);

            return _map.Map<ParametroEnvioViewModel>(model);
        }

        public async Task Deletar(int id)
        {
            await _repositorio.Deletar(id);
        }

        public async Task EnviarArquivoEmpresaCobranca(int id, string lote)
        {
            var parametroEnvio = await _repositorio.BuscarPorIdComRelacionamentos(id);
            var empresaParceira = parametroEnvio.EmpresaParceira;

            var senhaEnvioArquivo = await _encryptationApi.CallDecrypt(empresaParceira.SenhaEnvioArquivo);

            var dataGeracaoArquivo = DateTime.Now.ToString("dd/MM/yyyy");

            var geracaoArquivo = new GeracaoCobrancasModel()
            {
                CnpjEmpresaCobranca = parametroEnvio.EmpresaParceira.CNPJ,
                DataGeracao = dataGeracaoArquivo,
                DataInicio = parametroEnvio.InadimplenciaInicial.ToString("dd/MM/yyyy"),
                DataFim = parametroEnvio.InadimplenciaFinal.ToString("dd/MM/yyyy"),
                DataHoraEnvio = dataGeracaoArquivo,
                Sistema = parametroEnvio.Modalidade.CodigoMagister,
                Username = "APP_COBRANCA",
                TipoInadimplencia = parametroEnvio.TiposTitulos.First().CodigoMagister
            };

            await _geracaoCobrancaRepositorio.Criar(geracaoArquivo);

            using var client = new SftpClient(empresaParceira.IpEnvioArquivo, empresaParceira.PortaEnvioArquivo.Value,
                empresaParceira.UsuarioEnvioArquivo, senhaEnvioArquivo);
            try
            {
                client.Connect();

                foreach (var filename in await GerarArquivoCsv(parametroEnvio, lote, geracaoArquivo))
                {
                    using var s = File.OpenRead(filename);

                    client.UploadFile(s, "/mnt/Dados/unit/unit_usr/" + filename);

                    s.Close();
                }
            }
            catch (Renci.SshNet.Common.SshConnectionException sexc)
            {
                throw sexc;
            }
            catch (Renci.SshNet.Common.SftpPermissionDeniedException pexce)
            {
                throw pexce;
            }
            catch (Renci.SshNet.Common.SshException ssexc)
            {
                throw ssexc;
            }
            finally
            {
                client.Disconnect();
            }
        }

        public async Task<List<string>> GerarArquivoCsv(BuscaParametroEnvio parametroEnvio, string lote,
            GeracaoCobrancasModel geracaoCobrancas)
        {
            var arquivosGerados = new List<string>();
            var linhasGeradas = new List<ItensGeracaoModel>();

            var limiteLinhas = 999999;

            var filenameTemplate = "{0}_{1}_{2}_{3}_{4}_PARTE{5}de{6}.csv";

            var cabecalhoCsv =
                "\"CNPJ EMPRESA COBRANÇA\",\"MODALIDADE DE ENSINO\",\"DESCRIÇÃO DA MODALIDADE DE ENSINO\",\"IDENTIFICADOR INSTITUIÇÃO DE ENSINO\",\"DESCRIÇÃO INSTIUIÇÃO DE ENSINO\",\"IDENTIFICADOR CURSO\",\"DESCRIÇÃO CURSO\",\"TIPO TITULO\",\"DESCRIÇÃO TIPO TÍTULO\",\"TIPO TITULO AVULSO\",\"DESCRICAO INADIMPLENCIA\",\"SITUACAO ALUNO\",\"CPF ALUNO\",\"MATRICULA\",\"PERIODO\",\"IDENTIFICADOR DO ALUNO\",\"IDENTIFICADOR DA PESSOA\",\"NOME\",\"ENDERECO\",\"BAIRRO\",\"CIDADE\",\"CEP\",\"UF\",\"DDD RES\",\"TELEFONE RES\",\"DDD CELULAR\",\"TELEFONE CELULAR\",\"EMAIL DO ALUNO\",\"NUMERO CONTRATO\",\"PAGAMENTO A VISTA - DESCONTO NA MULTA E JUROS\",\"PAGAMENTO A VISTA - DESCONTO NO VALOR PRINCIPAL\",\"CARTÃO - DESCONTO NA MULTA E JUROS\",\"CARTÃO - DESCONTO NO VALOR PRINCIPAL\",\"CARTÃO - QUANTIDADE DE PARCELAS\",\"BOLETO - DESCONTO NA MULTA E JUROS\",\"BOLETO - DESCONTO NO VALOR PRINCIPAL\",\"BOLETO - ENTRADA\",\"BOLETO - QUANTIDADE DE PARCELAS\",\"DESCONTO INCONDICIONAL\",\"VALIDADE DA NEGOCIAÇÃO\",\"NUMERO DA PARCELA\",\"DATA VENCIMENTO\",\"VALOR PARCELA\",\"OBSERVACAO\",\"CODIGO DA CAMPUS IES\",\"NOME DA CAMPUS IES\",\"FILIACAO - MAE\",\"FILIACAO - PAI\",\"NUMERO DO RG\"";

            var dataTemplate =
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48}";

            var loteEnvio = await _repositorioLoteEnvio.GetLoteEnvio(lote);

            var dados = await _repositorioAlunosInadimplentes.GetAlunosInadimplentes();

            dados = dados
                .Where(alunos => alunos.Lote == loteEnvio.Lote)
                .ToList();

            var totalLinhas = dados.Count;
            var quantidadeArquivos = (int) (totalLinhas / limiteLinhas);

            if (totalLinhas > 0 && quantidadeArquivos == 0)
                quantidadeArquivos = 1;

            for (var indexArquivoAtual = 1; indexArquivoAtual <= quantidadeArquivos; indexArquivoAtual++)
            {
                var fileContent = new StringBuilder();
                var filename = string.Format(filenameTemplate, parametroEnvio.Instituicao.Id,
                    parametroEnvio.Modalidade.Id, DateTime.Now.ToString("dd-MM-yyyy"),
                    parametroEnvio.InadimplenciaInicial.ToString("dd-MM-yyyy"),
                    parametroEnvio.InadimplenciaFinal.ToString("dd-MM-yyyy"), indexArquivoAtual.ToString(),
                    quantidadeArquivos.ToString());
                using StreamWriter file = new(filename);
                await file.WriteLineAsync(cabecalhoCsv);
                fileContent.AppendLine(cabecalhoCsv);
                for (var indexDadosInicial = (indexArquivoAtual - 1) * limiteLinhas;
                     indexDadosInicial <= (indexArquivoAtual * limiteLinhas) - 1 && dados.Count > indexDadosInicial;
                     indexDadosInicial++)
                {
                    var alunoInadimplente = dados[indexDadosInicial];
                    var dataToWrite = string.Format(dataTemplate,
                        parametroEnvio.EmpresaParceira.CNPJ.Replace(",", "-"),
                        FormatarString(alunoInadimplente.CodModalidadeEnsino),
                        FormatarString(alunoInadimplente.DescricaoModalidadeEnsino),
                        loteEnvio.InstituicaoId.ToString(),
                        FormatarString(loteEnvio.Instituicao),
                        FormatarString(alunoInadimplente.CodCurso),
                        FormatarString(alunoInadimplente.NomeCurso),
                        FormatarString(alunoInadimplente.TipoInadimplencia),
                        FormatarString(alunoInadimplente.DescricaoTipoInadimplencia),
                        FormatarString(alunoInadimplente.TipoTituloAvulso),
                        FormatarString(alunoInadimplente.DescricaoInadimplencia),
                        FormatarString(alunoInadimplente.StatusAluno),
                        FormatarString(alunoInadimplente.CpfAluno),
                        FormatarString(alunoInadimplente.MatriculaAluno),
                        FormatarString(alunoInadimplente.Periodo),
                        FormatarString(alunoInadimplente.IdtAluno),
                        FormatarString(alunoInadimplente.IdtDdp),
                        FormatarString(alunoInadimplente.Nome),
                        FormatarString(alunoInadimplente.Endereco),
                        FormatarString(alunoInadimplente.Bairro),
                        FormatarString(alunoInadimplente.Cidade),
                        FormatarString(alunoInadimplente.Cep),
                        FormatarString(alunoInadimplente.Uf),
                        FormatarString(alunoInadimplente.DddResidencial),
                        FormatarString(alunoInadimplente.TelefoneResidencial),
                        FormatarString(alunoInadimplente.DddCelular),
                        FormatarString(alunoInadimplente.TelefoneCelular),
                        FormatarString(alunoInadimplente.Email),
                        FormatarString(alunoInadimplente.ChaveInadimplencia),
                        "5.0".Replace(",", " "),
                        "10.0".Replace(",", " "),
                        "5.0".Replace(",", " "),
                        "6.0".Replace(",", " "),
                        "12".Replace(",", " "),
                        "5.0".Replace(",", " "),
                        "10.0".Replace(",", " "),
                        "5.0".Replace(",", " "),
                        "10".Replace(",", " "),
                        alunoInadimplente.DescontoIncondicional == null
                            ? ""
                            : alunoInadimplente.DescontoIncondicional.Replace(".", "").Replace(",", "."),
                        "31/12/2021".Replace(",", " "),
                        alunoInadimplente.NumeroParcela == null
                            ? ""
                            : alunoInadimplente.NumeroParcela.Replace(",", " "),
                        alunoInadimplente.DataVencimento.ToShortDateString(),
                        alunoInadimplente.ValorPagamento == null
                            ? ""
                            : alunoInadimplente.ValorPagamento.Replace(".", "").Replace(",", "."),
                        FormatarString(alunoInadimplente.Observacao),
                        alunoInadimplente.IdtCampus == null ? "" : alunoInadimplente.IdtCampus.Replace(",", " "),
                        FormatarString(alunoInadimplente.DescricaoCampus),
                        FormatarString(alunoInadimplente.Mae),
                        FormatarString(alunoInadimplente.Pai),
                        alunoInadimplente.NumCi == null ? "" : alunoInadimplente.NumCi.Replace(",", " ")
                    );

                    try
                    {
                        var itemGeracao = new ItensGeracaoModel()
                        {
                            CnpjEmpresaCobranca = parametroEnvio.EmpresaParceira.CNPJ,
                            Controle = alunoInadimplente.MatriculaAluno +
                                       alunoInadimplente.NumeroParcela.PadLeft(3, '0'),
                            DataGeracao = Convert.ToDateTime(geracaoCobrancas.DataGeracao),
                            DataVencimento = alunoInadimplente.DataVencimento,
                            DescricaoInadimplencia = alunoInadimplente.DescricaoInadimplencia,
                            Matricula = Convert.ToDecimal(alunoInadimplente.MatriculaAluno),
                            Parcela = int.Parse(alunoInadimplente.NumeroParcela),
                            Sistema = parametroEnvio.Modalidade.CodigoMagister,
                            SituacaoAluno = alunoInadimplente.StatusAluno,
                            TipoInadimplencia = alunoInadimplente.TipoInadimplencia,
                            Valor = decimal.Parse(alunoInadimplente.ValorPagamento)
                        };

                        var periodo = -1;

                        if (Int32.TryParse(alunoInadimplente.Periodo, out periodo) && alunoInadimplente.Periodo.Length <= 6)
                        {
                            itemGeracao.Periodo = Convert.ToDecimal(alunoInadimplente.Periodo);
                            itemGeracao.PeriodoOutros = "1";
                        }
                        else
                        {
                            itemGeracao.Periodo = 1;
                            itemGeracao.PeriodoOutros = alunoInadimplente.Periodo;
                        }

                        linhasGeradas.Add(itemGeracao);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                    await file.WriteLineAsync(dataToWrite);
                    fileContent.AppendLine(dataToWrite);
                }

                await _itensGeracaoRepository.CriarVarios(linhasGeradas);

                file.Close();
                arquivosGerados.Add(filename);

                var arquivoGerado = new ArquivoCobrancasModel()
                {
                    Arquivo = fileContent.ToString(),
                    CnpjEmpresaCobranca = parametroEnvio.EmpresaParceira.CNPJ,
                    DataGeracao = geracaoCobrancas.DataGeracao,
                    Sequencia = indexArquivoAtual
                };
                await _arquivosGeracaoRepository.Criar(arquivoGerado);

                var conflitos = await _repositorioConflito.BuscarPorLote(loteEnvio.Lote.ToString());

                conflitos = conflitos.Select(x =>
                {
                    x.NomeLote = filename;
                    return x;
                }).ToList();

                await _repositorioConflito.AlterarVarios(conflitos);
            }

            return arquivosGerados;
        }

        private static string FormatarString(string dado)
        {
            if (string.IsNullOrEmpty(dado)) return string.Empty;

            var caracteres = dado
                .Replace(",", " ")
                .Normalize(NormalizationForm.FormD)
                .ToCharArray()
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray();

            return new string(caracteres).Normalize(NormalizationForm.FormC);
        }
    }
}