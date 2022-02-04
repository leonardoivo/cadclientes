using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Enum;
using Tiradentes.CobrancaAtiva.Domain.Exeptions;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class GerenciarArquivoCobrancaRetornoService : IGerenciarArquivoCobrancaRetornoService
    {
        readonly ICobrancaService _cobrancaService;
        readonly IParcelasAcordoService _parcelasAcordoService;
        readonly IAcordoCobrancaService _acordoCobrancaService;
        readonly IItensBaixasTipo1Service _itensBaixasTipo1Service;
        readonly IItensBaixasTipo2Service _itensBaixasTipo2Service;
        readonly IItensBaixasTipo3Service _itensBaixasTipo3Service;
        readonly IMatriculaAlunoExisteService _matriculaAlunoExisteService;
        readonly IItensGeracaoService _itensGeracaoService;
        readonly IParcelaTituloService _parcelaTituloService;
        readonly IParcelaPagaAlunoInstituicaoService _parcelaPagaAlunoInstituicao;
        readonly IBaixasCobrancasService _baixasCobrancasService;
        readonly IArquivoLayoutService _arquivolayoutService;

        private Dictionary<int, decimal> Erros { get; set; }

        public GerenciarArquivoCobrancaRetornoService(ICobrancaService cobrancaService,
            IParcelasAcordoService parcelasAcordoService,
            IAcordoCobrancaService acordoCobrancaService,
            IItensBaixasTipo1Service itensBaixasTipo1Service,
            IItensBaixasTipo2Service itensBaixasTipo2Service,
            IItensBaixasTipo3Service itensBaixasTipo3Service,
            IMatriculaAlunoExisteService matriculaAlunoExisteService,
            IItensGeracaoService itensGeracaoService,
            IParcelaTituloService parcelaTituloService,
            IParcelaPagaAlunoInstituicaoService parcelaPagaAlunoInstituicaoService,
            IBaixasCobrancasService baixasCobrancasService,
            IArquivoLayoutService arquivolayoutService)
        {
            _cobrancaService = cobrancaService;
            _parcelasAcordoService = parcelasAcordoService;
            _acordoCobrancaService = acordoCobrancaService;
            _itensBaixasTipo1Service = itensBaixasTipo1Service;
            _itensBaixasTipo2Service = itensBaixasTipo2Service;
            _itensBaixasTipo3Service = itensBaixasTipo3Service;
            _matriculaAlunoExisteService = matriculaAlunoExisteService;
            _itensGeracaoService = itensGeracaoService;
            _parcelaTituloService = parcelaTituloService;
            _parcelaPagaAlunoInstituicao = parcelaPagaAlunoInstituicaoService;
            _baixasCobrancasService = baixasCobrancasService;
            _arquivolayoutService = arquivolayoutService;

            Erros = new Dictionary<int, decimal>();
        }

        public async Task Gerenciar()
        {
            var respostas = await _cobrancaService.BuscarRepostaNaoIntegrada();
            if (!respostas.Any())
                return;

            var respostasAgrupadas = respostas.GroupBy(a => new {a.CnpjEmpresaCobranca, a.InstituicaoEnsino});

            foreach (var resp in respostasAgrupadas)
            {
                await GerenciaArquivos(resp.ToList(), resp.Key.CnpjEmpresaCobranca, resp.Key.InstituicaoEnsino);
            }
        }

        private async Task GerenciaArquivos(IList<RespostaViewModel> arquivos, string cnpjEmpresa, int ies)
        {
            var dataBaixa = DateTime.MinValue;
            try
            {
                var errosContabilizados = new List<ErroParcelaViewModel>();
                try
                {
                    dataBaixa = await _arquivolayoutService.SalvarLayoutArquivo("S",
                        JsonSerializer.Serialize(arquivos));
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException("Arquivo Layout existente", ex);
                }

                var model = await _baixasCobrancasService.Buscar(dataBaixa);

                if (model == null)
                {
                    await _baixasCobrancasService.CriarBaixasCobrancas(dataBaixa);
                }

                foreach (var arquivo in arquivos.OrderBy(A => A.TipoRegistro).ThenBy(A => A.Parcela))
                {
                    try
                    {
                        switch (arquivo.TipoRegistro)
                        {
                            case "1":
                                await ProcessaBaixaTipo1(dataBaixa, arquivo, errosContabilizados);
                                break;
                            case "2":
                                await ProcessaBaixaTipo2(dataBaixa, arquivo, errosContabilizados);
                                break;
                            case "3":
                                await ProcessaBaixaTipo3(dataBaixa, arquivo, errosContabilizados);
                                break;
                            default:
                                await _arquivolayoutService.RegistrarErro(dataBaixa, JsonSerializer.Serialize(arquivo),
                                    ErrosBaixaPagamento.ErroInternoServidor, "");
                                break;
                        }

                        arquivo.Integrado = true;
                        _cobrancaService.AlterarStatus(arquivo);
                    }
                    catch (Exception ex)
                    {
                        await _arquivolayoutService.RegistrarErro(dataBaixa, JsonSerializer.Serialize(ex.StackTrace),
                            ErrosBaixaPagamento.ErroInternoServidor, ex.Message);
                    }
                }

                await _baixasCobrancasService.AtualizarBaixasCobrancas(new BaixasCobrancasViewModel()
                {
                    DataBaixa = dataBaixa,
                    Etapa = 3,
                    QuantidadeTipo1 = arquivos.Count(A => A.TipoRegistro == "1"),
                    QuantidadeTipo2 = arquivos.Count(A => A.TipoRegistro == "2"),
                    QuantidadeTipo3 = arquivos.Count(A => A.TipoRegistro == "3"),

                    ValorTotalTipo1 = arquivos.Where(A => A.TipoRegistro == "1")
                        .Sum(A => Convert.ToDecimal(A.ValorParcela) / 100),
                    ValorTotalTipo2 = arquivos.Where(A => A.TipoRegistro == "2")
                        .Sum(A => Convert.ToDecimal(A.ValorParcela) / 100),
                    ValorTotalTipo3 = arquivos.Where(A => A.TipoRegistro == "3")
                        .Sum(A => Convert.ToDecimal(A.ValorParcela) / 100),

                    QuantidadeErrosTipo1 = errosContabilizados.Count(E => E.Etapa == 1),
                    QuantidadeErrosTipo2 = errosContabilizados.Count(E => E.Etapa == 2),
                    QuantidadeErrosTipo3 = errosContabilizados.Count(E => E.Etapa == 3),

                    ValorTotalErrosTipo1 = errosContabilizados.Where(E => E.Etapa == 1).Sum(E => E.ValorParcela),
                    ValorTotalErrosTipo2 = errosContabilizados.Where(E => E.Etapa == 2).Sum(E => E.ValorParcela),
                    ValorTotalErrosTipo3 = errosContabilizados.Where(E => E.Etapa == 3).Sum(E => E.ValorParcela),
                    UserName = ""
                });
            }
            catch (ArgumentNullException ex)
            {
                var dataErro =
                    await _arquivolayoutService.SalvarLayoutArquivo("E", "Arquivo ja processado com a data de hoje");
                await _arquivolayoutService.RegistrarErro(dataErro, JsonSerializer.Serialize(ex.StackTrace),
                    ErrosBaixaPagamento.OutrosErros, ex.Message);
            }
            catch (Exception ex)
            {
                await _arquivolayoutService.RegistrarErro(dataBaixa, JsonSerializer.Serialize(ex.StackTrace),
                    ErrosBaixaPagamento.ErroInternoServidor, ex.Message);
            }
            finally
            {
                await _arquivolayoutService.AlterarConteudo(dataBaixa, arquivos);
            }
        }

        private int? GetPeriodoChequeDevolvido(string periodo)
        {
            if (string.IsNullOrEmpty(periodo))
            {
                return null;
            }

            return Convert.ToInt32(periodo);
        }

        private async Task ProcessaBaixaTipo1(DateTime dataBaixa, RespostaViewModel resposta,
            List<ErroParcelaViewModel> erros)
        {
            int codErro = 0;

            var arquivo = new
            {
                resposta.TipoRegistro,
                resposta.CPF,
                NumeroAcordo =
                    Convert.ToInt64(!string.IsNullOrEmpty(resposta.NumeroAcordo) ? resposta.NumeroAcordo : "0"),
                Parcela = Convert.ToInt32(!string.IsNullOrEmpty(resposta.Parcela) ? resposta.Parcela : "0"),
                resposta.CnpjEmpresaCobranca,
                SituacaoAluno = !string.IsNullOrEmpty(resposta.SituacaoAluno) ? resposta.SituacaoAluno : "",
                resposta.Sistema,
                Matricula = Convert.ToInt64(!string.IsNullOrEmpty(resposta.Matricula) ? resposta.Matricula : "0"),
                Periodo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Periodo) ? resposta.Periodo : "0"),
                IdTitulo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdTitulo) ? resposta.IdTitulo : "0"),
                CodigoAtividade =
                    Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAtividade) ? resposta.CodigoAtividade : "0"),
                NumeroEvt = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroEvt) ? resposta.NumeroEvt : "0"),
                IdPessoa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdPessoa) ? resposta.IdPessoa : "0"),
                CodigoBanco = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoBanco) ? resposta.CodigoBanco : "0"),
                CodigoAgencia =
                    Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAgencia) ? resposta.CodigoAgencia : "0"),
                NumeroConta = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroConta) ? resposta.NumeroConta : "0"),
                NumeroCheque =
                    Convert.ToDecimal(!string.IsNullOrEmpty(resposta.NumeroCheque) ? resposta.NumeroCheque : "0"),
                resposta.TipoInadimplencia,
                resposta.ChaveInadimplencia,
                Juros = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Juros) ? resposta.Juros : "0") / 100,
                Multa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Multa) ? resposta.Multa : "0") / 100,
                ValorTotal = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorTotal) ? resposta.ValorTotal : "0") /
                             100,
                DataFechamentoAcordo = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataFechamentoAcordo)
                    ? DateTime.ParseExact(resposta.DataFechamentoAcordo, "ddMMyyyy", CultureInfo.InvariantCulture)
                    : "01-01-0001"),
                TotalParcelas =
                    Convert.ToInt32(!string.IsNullOrEmpty(resposta.TotalParcelas) ? resposta.TotalParcelas : "0"),
                DataVencimento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataVencimento)
                    ? DateTime.ParseExact(resposta.DataVencimento, "ddMMyyyy", CultureInfo.InvariantCulture)
                    : "01-01-0001"),
                ValorParcela =
                    Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorParcela) ? resposta.ValorParcela : "0") / 100,
                SaldoDevedorTotal =
                    Convert.ToDecimal(!string.IsNullOrEmpty(resposta.SaldoDevedorTotal)
                        ? resposta.SaldoDevedorTotal
                        : "0") / 100,
                resposta.Produto,
                resposta.DescricaoProduto,
                resposta.Fase,
                resposta.CodigoControleCliente,
                resposta.NossoNumero,
                DataPagamento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataPagamento)
                    ? DateTime.ParseExact(resposta.DataPagamento, "ddMMyyyy", CultureInfo.InvariantCulture)
                    : "01-01-0001"),
                DataBaixa = dataBaixa,
                ValorPago = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorPago) ? resposta.ValorPago : "0") /
                            100,
                resposta.TipoPagamento
            };

            try
            {
                if (_parcelasAcordoService.ExisteParcelaAcordo(Convert.ToDecimal(arquivo.Parcela),
                        Convert.ToDecimal(arquivo.NumeroAcordo)))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.ParcelaJaCadastrada);
                }

                if (!_acordoCobrancaService.ExisteAcordo(Convert.ToDecimal(arquivo.NumeroAcordo)))
                {
                    await _acordoCobrancaService.InserirAcordoCobranca(arquivo.NumeroAcordo,
                        arquivo.DataBaixa,
                        arquivo.DataFechamentoAcordo,
                        arquivo.TotalParcelas,
                        arquivo.ValorTotal,
                        arquivo.Multa,
                        arquivo.Juros,
                        arquivo.Matricula,
                        arquivo.SaldoDevedorTotal,
                        arquivo.CPF,
                        arquivo.CnpjEmpresaCobranca,
                        arquivo.Sistema,
                        arquivo.TipoInadimplencia);
                }


                //(Se o acordo existe e é a primeira parcela) ou (se existe acordo)
                //Apenas para a 1º parcelas ou para todas?
                await _parcelasAcordoService.InserirPagamentoParcelaAcordo(arquivo.Parcela,
                    arquivo.NumeroAcordo,
                    arquivo.Sistema,
                    arquivo.DataBaixa,
                    arquivo.DataVencimento,
                    arquivo.ValorParcela,
                    arquivo.CnpjEmpresaCobranca,
                    arquivo.TipoInadimplencia);
            }
            catch (ErroArquivoCobrancaException ex)
            {
                resposta.Erro = (int) ex.Erro;
                var idErroLayout = await _arquivolayoutService.RegistrarErro(arquivo.DataBaixa,
                    JsonSerializer.Serialize(resposta), ex.Erro, ex.Message);

                codErro = (int) ex.Erro;

                erros.Add(new ErroParcelaViewModel()
                {
                    Etapa = 1,
                    ValorParcela = arquivo.ValorParcela,
                    IdErro = idErroLayout ?? 0,
                    CodErro = codErro
                });
            }


            await _itensBaixasTipo1Service.InserirBaixa(arquivo.DataBaixa,
                arquivo.Matricula,
                arquivo.NumeroAcordo,
                arquivo.Multa,
                arquivo.Juros,
                arquivo.DataVencimento,
                arquivo.ValorParcela,
                codErro,
                arquivo.CnpjEmpresaCobranca,
                arquivo.Parcela,
                arquivo.Sistema,
                arquivo.SituacaoAluno,
                arquivo.TipoInadimplencia
            );
        }

        private async Task ProcessaBaixaTipo2(DateTime dataBaixa, RespostaViewModel resposta,
            List<ErroParcelaViewModel> erros)
        {
            int codErro = 0;

            var arquivo = new
            {
                resposta.TipoRegistro,
                resposta.CPF,
                NumeroAcordo =
                    Convert.ToInt64(!string.IsNullOrEmpty(resposta.NumeroAcordo) ? resposta.NumeroAcordo : "0"),
                Parcela = Convert.ToInt32(!string.IsNullOrEmpty(resposta.Parcela) ? resposta.Parcela : "0"),
                resposta.CnpjEmpresaCobranca,
                SituacaoAluno = !string.IsNullOrEmpty(resposta.SituacaoAluno) ? resposta.SituacaoAluno : "",
                resposta.Sistema,
                Matricula = Convert.ToInt64(!string.IsNullOrEmpty(resposta.Matricula) ? resposta.Matricula : "0"),
                Periodo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Periodo) ? resposta.Periodo : "0"),
                IdTitulo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdTitulo) ? resposta.IdTitulo : "0"),
                CodigoAtividade =
                    Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAtividade) ? resposta.CodigoAtividade : "0"),
                NumeroEvt = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroEvt) ? resposta.NumeroEvt : "0"),
                IdPessoa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdPessoa) ? resposta.IdPessoa : "0"),
                CodigoBanco = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoBanco) ? resposta.CodigoBanco : "0"),
                CodigoAgencia =
                    Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAgencia) ? resposta.CodigoAgencia : "0"),
                NumeroConta = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroConta) ? resposta.NumeroConta : "0"),
                NumeroCheque =
                    Convert.ToDecimal(!string.IsNullOrEmpty(resposta.NumeroCheque) ? resposta.NumeroCheque : "0"),
                resposta.TipoInadimplencia,
                resposta.ChaveInadimplencia,
                Juros = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Juros) ? resposta.Juros : "0") / 100,
                Multa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Multa) ? resposta.Multa : "0") / 100,
                ValorTotal = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorTotal) ? resposta.ValorTotal : "0") /
                             100,
                DataFechamentoAcordo = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataFechamentoAcordo)
                    ? DateTime.ParseExact(resposta.DataFechamentoAcordo, "ddMMyyyy", CultureInfo.InvariantCulture)
                    : "01-01-0001"),
                TotalParcelas =
                    Convert.ToInt32(!string.IsNullOrEmpty(resposta.TotalParcelas) ? resposta.TotalParcelas : "0"),
                DataVencimento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataVencimento)
                    ? DateTime.ParseExact(resposta.DataVencimento, "ddMMyyyy", CultureInfo.InvariantCulture)
                    : "01-01-0001"),
                ValorParcela =
                    Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorParcela) ? resposta.ValorParcela : "0") / 100,
                SaldoDevedorTotal =
                    Convert.ToDecimal(!string.IsNullOrEmpty(resposta.SaldoDevedorTotal)
                        ? resposta.SaldoDevedorTotal
                        : "0") / 100,
                resposta.Produto,
                resposta.DescricaoProduto,
                resposta.Fase,
                resposta.CodigoControleCliente,
                resposta.NossoNumero,
                DataPagamento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataPagamento)
                    ? DateTime.ParseExact(resposta.DataPagamento, "ddMMyyyy", CultureInfo.InvariantCulture)
                    : "01-01-0001"),
                DataBaixa = dataBaixa,
                ValorPago = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorPago) ? resposta.ValorPago : "0") /
                            100,
                resposta.TipoPagamento,
                PeriodoChequeDevolvido = GetPeriodoChequeDevolvido(resposta.PeriodoChequeDevolvido)
            };

            try
            {
                if (!_matriculaAlunoExisteService.MatriculaAlunoExiste(arquivo.TipoInadimplencia,
                        arquivo.Sistema,
                        arquivo.Matricula))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.MatriculaInexistente);
                }

                var dataEnvio = _itensGeracaoService.ObterDataEnvio(arquivo.CnpjEmpresaCobranca,
                    arquivo.Matricula,
                    arquivo.Periodo,
                    arquivo.Parcela);

                if (dataEnvio.Date != Convert.ToDateTime(arquivo.DataVencimento).Date)
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.DataInconsistente);
                }

                if (_parcelaTituloService.ExisteParcela(arquivo.Matricula, arquivo.Periodo, arquivo.Parcela))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento
                        .ParcelaEnviadaAnteriormentePelaEmpresaCobranca);
                }

                if (_parcelaPagaAlunoInstituicao.ParcelaPagaInstituicao(tipoInadimplencia: arquivo.TipoInadimplencia,
                        sistema: arquivo.Sistema,
                        matricula: arquivo.Matricula,
                        periodo: arquivo.Periodo,
                        parcela: arquivo.Parcela,
                        idTitulo: arquivo.IdTitulo,
                        codigoAtividade: arquivo.CodigoAtividade,
                        numeroEvt: arquivo.NumeroEvt,
                        idPessoa: arquivo.IdPessoa,
                        codigoBanco: arquivo.CodigoBanco,
                        codigoAgencia: arquivo.CodigoAgencia,
                        numeroConta: arquivo.NumeroConta,
                        numeroCheque: arquivo.NumeroCheque
                    ))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.ParcelaPagaInstituicao);
                }

                if (!_itensGeracaoService.ExisteMatricula(arquivo.CnpjEmpresaCobranca,
                        arquivo.Matricula,
                        arquivo.Periodo,
                        arquivo.Parcela))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.GeracaoInconsistente);
                }

                if (_acordoCobrancaService.ExisteAcordo(Convert.ToDecimal(arquivo.NumeroAcordo)))
                {
                    await _acordoCobrancaService.AtualizarMatriculaAcordo(arquivo.Matricula, arquivo.NumeroAcordo);

                    await _itensBaixasTipo1Service.AtualizarMatricula(arquivo.DataBaixa, arquivo.NumeroAcordo,
                        arquivo.Matricula);
                }

                await _parcelaTituloService.InserirParcela(arquivo.NumeroAcordo,
                    arquivo.Matricula,
                    arquivo.Periodo,
                    arquivo.Parcela,
                    arquivo.DataBaixa,
                    dataEnvio,
                    arquivo.DataVencimento,
                    arquivo.ValorParcela,
                    arquivo.CnpjEmpresaCobranca,
                    arquivo.Sistema,
                    arquivo.TipoInadimplencia,
                    arquivo.PeriodoChequeDevolvido);

                if (_parcelasAcordoService.ExisteParcelaPaga(Convert.ToDecimal(arquivo.NumeroAcordo)))
                {
                    await _parcelasAcordoService.QuitarParcelasAcordo(numeroAcordo: arquivo.NumeroAcordo,
                        matricula: arquivo.Matricula,
                        sistema: arquivo.Sistema,
                        dataPagamento: arquivo.DataPagamento,
                        periodo: arquivo.Periodo,
                        idTitulo: arquivo.IdTitulo,
                        codigoAtividade: arquivo.CodigoAtividade,
                        numeroEvt: arquivo.NumeroEvt,
                        idPessoa: arquivo.IdPessoa,
                        codigobanco: arquivo.CodigoBanco,
                        codigoAgencia: arquivo.CodigoAgencia,
                        numeroConta: arquivo.NumeroConta,
                        numeroCheque: arquivo.NumeroCheque,
                        CpfCnpj: arquivo.CPF
                    );
                }
            }
            catch (ErroArquivoCobrancaException ex)
            {
                resposta.Erro = (int) ex.Erro;
                var idErroLayout = await _arquivolayoutService.RegistrarErro(arquivo.DataBaixa,
                    JsonSerializer.Serialize(resposta), ex.Erro, ex.Message);

                codErro = (int) ex.Erro;

                erros.Add(new ErroParcelaViewModel()
                {
                    Etapa = 2,
                    ValorParcela = arquivo.ValorParcela,
                    IdErro = idErroLayout ?? 0,
                    CodErro = codErro
                });
            }


            await _itensBaixasTipo2Service.InserirBaixa(arquivo.DataBaixa,
                arquivo.Matricula,
                arquivo.NumeroAcordo,
                arquivo.Parcela,
                arquivo.Periodo,
                arquivo.DataVencimento,
                arquivo.ValorPago,
                codErro,
                arquivo.CnpjEmpresaCobranca,
                arquivo.Sistema,
                arquivo.SituacaoAluno,
                arquivo.TipoInadimplencia,
                arquivo.PeriodoChequeDevolvido);
        }

        private async Task ProcessaBaixaTipo3(DateTime dataBaixa, RespostaViewModel resposta,
            List<ErroParcelaViewModel> erros)
        {
            int codErro = 0;

            var arquivo = new
            {
                resposta.TipoRegistro,
                resposta.CPF,
                NumeroAcordo =
                    Convert.ToInt64(!string.IsNullOrEmpty(resposta.NumeroAcordo) ? resposta.NumeroAcordo : "0"),
                Parcela = Convert.ToInt32(!string.IsNullOrEmpty(resposta.Parcela) ? resposta.Parcela : "0"),
                resposta.CnpjEmpresaCobranca,
                SituacaoAluno = !string.IsNullOrEmpty(resposta.SituacaoAluno) ? resposta.SituacaoAluno : "",
                resposta.Sistema,
                Matricula = Convert.ToInt64(!string.IsNullOrEmpty(resposta.Matricula) ? resposta.Matricula : "0"),
                Periodo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Periodo) ? resposta.Periodo : "0"),
                IdTitulo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdTitulo) ? resposta.IdTitulo : "0"),
                CodigoAtividade =
                    Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAtividade) ? resposta.CodigoAtividade : "0"),
                NumeroEvt = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroEvt) ? resposta.NumeroEvt : "0"),
                IdPessoa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdPessoa) ? resposta.IdPessoa : "0"),
                CodigoBanco = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoBanco) ? resposta.CodigoBanco : "0"),
                CodigoAgencia =
                    Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAgencia) ? resposta.CodigoAgencia : "0"),
                NumeroConta = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroConta) ? resposta.NumeroConta : "0"),
                NumeroCheque =
                    Convert.ToDecimal(!string.IsNullOrEmpty(resposta.NumeroCheque) ? resposta.NumeroCheque : "0"),
                resposta.TipoInadimplencia,
                resposta.ChaveInadimplencia,
                Juros = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Juros) ? resposta.Juros : "0") / 100,
                Multa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Multa) ? resposta.Multa : "0") / 100,
                ValorTotal = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorTotal) ? resposta.ValorTotal : "0") /
                             100,
                DataFechamentoAcordo = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataFechamentoAcordo)
                    ? DateTime.ParseExact(resposta.DataFechamentoAcordo, "ddMMyyyy", CultureInfo.InvariantCulture)
                    : "01-01-0001"),
                TotalParcelas =
                    Convert.ToInt32(!string.IsNullOrEmpty(resposta.TotalParcelas) ? resposta.TotalParcelas : "0"),
                DataVencimento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataVencimento)
                    ? DateTime.ParseExact(resposta.DataVencimento, "ddMMyyyy", CultureInfo.InvariantCulture)
                    : "01-01-0001"),
                ValorParcela =
                    Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorParcela) ? resposta.ValorParcela : "0") / 100,
                SaldoDevedorTotal =
                    Convert.ToDecimal(!string.IsNullOrEmpty(resposta.SaldoDevedorTotal)
                        ? resposta.SaldoDevedorTotal
                        : "0") / 100,
                resposta.Produto,
                resposta.DescricaoProduto,
                resposta.Fase,
                resposta.CodigoControleCliente,
                resposta.NossoNumero,
                DataPagamento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataPagamento)
                    ? DateTime.ParseExact(resposta.DataPagamento, "ddMMyyyy", CultureInfo.InvariantCulture)
                    : "01-01-0001"),
                DataBaixa = dataBaixa,
                ValorPago = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorPago) ? resposta.ValorPago : "0") /
                            100,
                resposta.TipoPagamento
            };

            try
            {
                var valorParcelaAcordo =
                    _parcelasAcordoService.ObterValorParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo);

                if (valorParcelaAcordo == null)
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.AcordoNaoCadastrado);
                }

                if (arquivo.ValorPago < valorParcelaAcordo)
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.ValorPagoInsuficiente);
                }

                if (_parcelasAcordoService.ParcelaPaga(arquivo.Parcela, arquivo.NumeroAcordo))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.ParcelaAcordoJaPaga);
                }

                if (!_parcelaTituloService.ExisteParcelaInadimplente(arquivo.DataBaixa))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.NaoExisteParcelasVinculadas);
                }

                try
                {
                    await _parcelasAcordoService.AtualizaPagamentoParcelaAcordo(arquivo.Parcela,
                        arquivo.NumeroAcordo,
                        arquivo.DataPagamento,
                        arquivo.DataBaixa,
                        arquivo.ValorPago);

                    var valorParcela =
                        _parcelasAcordoService.ObterValorParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo);

                    await _acordoCobrancaService.AtualizarSaldoDevedor(arquivo.NumeroAcordo, (valorParcela ?? 0) * -1);

                    if (arquivo.Parcela == 1)
                    {
                        try
                        {
                            await _parcelasAcordoService.QuitarParcelasAcordo(numeroAcordo: arquivo.NumeroAcordo,
                                matricula: arquivo.Matricula,
                                sistema: arquivo.Sistema,
                                dataPagamento: arquivo.DataPagamento,
                                periodo: arquivo.Periodo,
                                idTitulo: arquivo.IdTitulo,
                                codigoAtividade: arquivo.CodigoAtividade,
                                numeroEvt: arquivo.NumeroEvt,
                                idPessoa: arquivo.IdPessoa,
                                codigobanco: arquivo.CodigoBanco,
                                codigoAgencia: arquivo.CodigoAgencia,
                                numeroConta: arquivo.NumeroConta,
                                numeroCheque: arquivo.NumeroCheque,
                                CpfCnpj: arquivo.CPF
                            );
                        }
                        catch (Exception)
                        {
                            await _acordoCobrancaService.AtualizarSaldoDevedor(arquivo.NumeroAcordo, valorParcela ?? 0);

                            throw;
                        }
                    }
                }
                catch (Exception)
                {
                    await _parcelasAcordoService.EstornarParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo);

                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.OutrosErros);
                }
            }
            catch (ErroArquivoCobrancaException ex)
            {
                resposta.Erro = (int) ex.Erro;
                var idErroLayout = await _arquivolayoutService.RegistrarErro(arquivo.DataBaixa,
                    JsonSerializer.Serialize(resposta), ex.Erro, ex.Message);

                codErro = (int) ex.Erro;

                erros.Add(new ErroParcelaViewModel()
                {
                    Etapa = 3,
                    ValorParcela = arquivo.ValorParcela,
                    IdErro = idErroLayout ?? 0,
                    CodErro = codErro
                });
            }

            await _itensBaixasTipo3Service.InserirBaixa(arquivo.DataBaixa,
                arquivo.Matricula,
                arquivo.NumeroAcordo,
                arquivo.Parcela,
                arquivo.DataPagamento,
                arquivo.ValorPago,
                codErro,
                arquivo.CnpjEmpresaCobranca,
                arquivo.Sistema,
                arquivo.SituacaoAluno,
                arquivo.TipoInadimplencia,
                arquivo.TipoPagamento);
        }
    }
}