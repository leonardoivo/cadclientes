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

            var respostasAgrupadas = respostas.GroupBy(a => new {a.CnpjEmpresaCobranca, a.CodigoInstituicaoEnsino});

            foreach (var resp in respostasAgrupadas)
            {
                await GerenciaArquivos(resp.ToList(), resp.Key.CnpjEmpresaCobranca, resp.Key.CodigoInstituicaoEnsino);
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
                        JsonSerializer.Serialize(arquivos), cnpjEmpresa, ies);
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
                            case 1:
                                await ProcessaBaixaTipo1(dataBaixa, arquivo, errosContabilizados);
                                break;
                            case 2:
                                await ProcessaBaixaTipo2(dataBaixa, arquivo, errosContabilizados);
                                break;
                            case 3:
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
                    QuantidadeTipo1 = arquivos.Count(A => A.TipoRegistro == 1),
                    QuantidadeTipo2 = arquivos.Count(A => A.TipoRegistro == 2),
                    QuantidadeTipo3 = arquivos.Count(A => A.TipoRegistro == 3),

                    ValorTotalTipo1 = arquivos.Where(A => A.TipoRegistro == 1)
                        .Sum(A => Convert.ToDecimal(A.ValorParcela) / 100),
                    ValorTotalTipo2 = arquivos.Where(A => A.TipoRegistro == 2)
                        .Sum(A => Convert.ToDecimal(A.ValorParcela) / 100),
                    ValorTotalTipo3 = arquivos.Where(A => A.TipoRegistro == 3)
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
                    await _arquivolayoutService.SalvarLayoutArquivo("E", "Arquivo ja processado com a data de hoje",
                        cnpjEmpresa, ies);
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
        
        private async Task ProcessaBaixaTipo1(DateTime dataBaixa, RespostaViewModel resposta, List<ErroParcelaViewModel> erros)
        {
            int codErro = 0;

            var arquivo = new
            {
                TipoRegistro = resposta.TipoRegistro,
                CPF = resposta.CPF,
                NumeroAcordo = resposta.NumeroAcordo,
                Parcela = resposta.Parcela,
                CnpjEmpresaCobranca = resposta.CnpjEmpresaCobranca,
                SituacaoAluno = resposta.SituacaoAluno,
                Sistema = resposta.Sistema,
                Matricula = resposta.Matricula,
                Periodo = resposta.Periodo,
                IdTitulo = resposta.IdTitulo,
                CodigoAtividade = resposta.CodigoAtividade,
                NumeroEvt = resposta.NumeroEvt,
                IdPessoa = resposta.IdPessoa,
                CodigoBanco = resposta.CodigoBanco,
                CodigoAgencia = resposta.CodigoAgencia,
                NumeroConta = resposta.NumeroConta,
                NumeroCheque = resposta.NumeroCheque,
                TipoInadimplencia = resposta.TipoInadimplencia,
                ChaveInadimplencia = resposta.ChaveInadimplencia,
                Juros = resposta.JurosParcela,
                Multa = resposta.MultaParcela,
                ValorTotal = resposta.ValorTotalParcela,
                DataFechamentoAcordo = resposta.DataFechamentoAcordo,
                TotalParcelas = resposta.TotalParcelasAcordo,
                DataVencimento = resposta.DataVencimentoParcela,
                ValorParcela = resposta.ValorParcela,
                SaldoDevedorTotal = resposta.SaldoDevedorTotal,
                Produto = resposta.Produto,
                DescricaoProduto = resposta.DescricaoProduto,
                CodigoControleCliente = resposta.CodigoControleCliente,
                NossoNumero = resposta.NossoNumero,
                DataPagamento = resposta.DataPagamento,
                DataBaixa = dataBaixa,
                ValorPago = resposta.ValorPago,
                TipoPagamento = resposta.TipoPagamento
            };

            try
            {

                if (_parcelasAcordoService.ExisteParcelaAcordo(Convert.ToDecimal(arquivo.Parcela), Convert.ToDecimal(arquivo.NumeroAcordo)))
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
                                                                       arquivo.CPF.ToString(),
                                                                       arquivo.CnpjEmpresaCobranca.ToString(),
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
                                                                           arquivo.CnpjEmpresaCobranca.ToString(),
                                                                           arquivo.TipoInadimplencia);
            }
            catch (ErroArquivoCobrancaException ex)
            {

                var idErroLayout = await _arquivolayoutService.RegistrarErro(arquivo.DataBaixa, JsonSerializer.Serialize(resposta), ex.Erro, ex.Message);

                codErro = (int)ex.Erro;

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
                                                  arquivo.CnpjEmpresaCobranca.ToString(),
                                                  arquivo.Parcela,
                                                  arquivo.Sistema,
                                                  arquivo.SituacaoAluno,
                                                  arquivo.TipoInadimplencia
                                                  );


        }

        private async Task ProcessaBaixaTipo2(DateTime dataBaixa, RespostaViewModel resposta, List<ErroParcelaViewModel> erros)
        {
            int codErro = 0;

            var arquivo = new
            {
                TipoRegistro = resposta.TipoRegistro,
                CPF = resposta.CPF,
                NumeroAcordo = resposta.NumeroAcordo,
                Parcela = resposta.Parcela,
                CnpjEmpresaCobranca = resposta.CnpjEmpresaCobranca,
                SituacaoAluno = !string.IsNullOrEmpty(resposta.SituacaoAluno) ? resposta.SituacaoAluno : "",
                Sistema = resposta.Sistema,
                Matricula = resposta.Matricula,
                Periodo = resposta.ObterPeriodo(),
                IdTitulo = resposta.IdTitulo,
                CodigoAtividade = resposta.CodigoAtividade,
                NumeroEvt = resposta.NumeroEvt,
                IdPessoa = resposta.IdPessoa,
                CodigoBanco = resposta.CodigoBanco,
                CodigoAgencia = resposta.CodigoAgencia,
                NumeroConta = resposta.NumeroConta,
                NumeroCheque = resposta.NumeroCheque,
                TipoInadimplencia = resposta.TipoInadimplencia,
                ChaveInadimplencia = resposta.ChaveInadimplencia,
                Juros = resposta.JurosParcela,
                Multa = resposta.MultaParcela,
                ValorTotal = resposta.ValorTotalParcela,
                DataFechamentoAcordo = resposta.DataFechamentoAcordo,
                TotalParcelas = resposta.TotalParcelasAcordo,
                DataVencimento = resposta.DataVencimentoParcela,
                ValorParcela = resposta.ValorParcela,
                SaldoDevedorTotal = resposta.SaldoDevedorTotal,
                Produto = resposta.Produto,
                DescricaoProduto = resposta.DescricaoProduto,
                CodigoControleCliente = resposta.CodigoControleCliente,
                NossoNumero = resposta.NossoNumero,
                DataPagamento = resposta.DataPagamento,
                DataBaixa = dataBaixa,
                ValorPago = resposta.ValorPago,
                TipoPagamento = resposta.TipoPagamento,
                PeriodoOutros = resposta.ObterPeriodoOutros()
            };

            try
            {

                if (!_matriculaAlunoExisteService.MatriculaAlunoExiste(arquivo.TipoInadimplencia,
                                                                       arquivo.Sistema,
                                                                       arquivo.Matricula))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.MatriculaInexistente);
                }

                var dataEnvio = _itensGeracaoService.ObterDataEnvio(arquivo.CnpjEmpresaCobranca.ToString(),
                                                                   arquivo.Matricula,
                                                                   arquivo.Periodo,
                                                                   arquivo.Parcela,
                                                                   arquivo.PeriodoOutros);

                if (dataEnvio.Date != Convert.ToDateTime(arquivo.DataVencimento).Date)
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.DataInconsistente);
                }

                if (_parcelaTituloService.ExisteParcela(arquivo.Matricula, arquivo.Periodo, arquivo.Parcela, arquivo.PeriodoOutros))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.ParcelaEnviadaAnteriormentePelaEmpresaCobranca);
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

                if (!_itensGeracaoService.ExisteMatricula(arquivo.CnpjEmpresaCobranca.ToString(),
                                                        arquivo.Matricula,
                                                        arquivo.Periodo,
                                                        arquivo.Parcela,
                                                        arquivo.PeriodoOutros))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.GeracaoInconsistente);
                }

                if (_acordoCobrancaService.ExisteAcordo(Convert.ToDecimal(arquivo.NumeroAcordo)))
                {
                    await _acordoCobrancaService.AtualizarMatriculaAcordo(arquivo.Matricula, arquivo.NumeroAcordo);

                    await _itensBaixasTipo1Service.AtualizarMatricula(arquivo.DataBaixa, arquivo.NumeroAcordo, arquivo.Matricula);
                }

                await _parcelaTituloService.InserirParcela(arquivo.NumeroAcordo,
                                                     arquivo.Matricula,
                                                     arquivo.Periodo,
                                                     arquivo.Parcela,
                                                     arquivo.DataBaixa,
                                                     dataEnvio,
                                                     arquivo.DataVencimento,
                                                     arquivo.ValorParcela,
                                                     arquivo.CnpjEmpresaCobranca.ToString(),
                                                     arquivo.Sistema,
                                                     arquivo.TipoInadimplencia,
                                                     arquivo.PeriodoOutros);

                if (_parcelasAcordoService.ExisteParcelaPaga(Convert.ToDecimal(arquivo.NumeroAcordo)))
                {

                    await _parcelasAcordoService.QuitarParcelasAcordo(numeroAcordo: arquivo.NumeroAcordo,
                                                                matricula: arquivo.Matricula,
                                                                sistema: arquivo.Sistema,
                                                                dataPagamento: Convert.ToDateTime(arquivo.DataPagamento != null ? DateTime.ParseExact(arquivo.DataPagamento.ToString(), "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                                                                periodo: arquivo.Periodo,
                                                                idTitulo: arquivo.IdTitulo,
                                                                codigoAtividade: arquivo.CodigoAtividade,
                                                                numeroEvt: arquivo.NumeroEvt,
                                                                idPessoa: arquivo.IdPessoa,
                                                                codigobanco: arquivo.CodigoBanco,
                                                                codigoAgencia: arquivo.CodigoAgencia,
                                                                numeroConta: arquivo.NumeroConta,
                                                                numeroCheque: arquivo.NumeroCheque,
                                                                CpfCnpj: arquivo.CPF.ToString()
                                                                );
                }
            }
            catch (ErroArquivoCobrancaException ex)
            {

                var idErroLayout = await _arquivolayoutService.RegistrarErro(arquivo.DataBaixa, JsonSerializer.Serialize(resposta), ex.Erro, ex.Message);

                codErro = (int)ex.Erro;

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
                                                  arquivo.CnpjEmpresaCobranca.ToString(),
                                                  arquivo.Sistema,
                                                  arquivo.SituacaoAluno,
                                                  arquivo.TipoInadimplencia,
                                                  arquivo.PeriodoOutros);
        }

        private async Task ProcessaBaixaTipo3(DateTime dataBaixa, RespostaViewModel resposta, List<ErroParcelaViewModel> erros)
        {
            int codErro = 0;

            var arquivo = new
            {
                TipoRegistro = resposta.TipoRegistro,
                CPF = resposta.CPF,
                NumeroAcordo = resposta.NumeroAcordo,
                Parcela = resposta.Parcela,
                CnpjEmpresaCobranca = resposta.CnpjEmpresaCobranca,
                SituacaoAluno = !string.IsNullOrEmpty(resposta.SituacaoAluno) ? resposta.SituacaoAluno : "",
                Sistema = resposta.Sistema,
                Matricula = resposta.Matricula,
                Periodo = resposta.ObterPeriodo(),
                IdTitulo = resposta.IdTitulo,
                CodigoAtividade = resposta.CodigoAtividade,
                NumeroEvt = resposta.NumeroEvt,
                IdPessoa = resposta.IdPessoa,
                CodigoBanco = resposta.CodigoBanco,
                CodigoAgencia = resposta.CodigoAgencia,
                NumeroConta = resposta.NumeroConta,
                NumeroCheque = resposta.NumeroCheque,
                TipoInadimplencia = resposta.TipoInadimplencia,
                ChaveInadimplencia = resposta.ChaveInadimplencia,
                Juros = resposta.JurosParcela,
                Multa = resposta.MultaParcela,
                ValorTotal = resposta.ValorTotalParcela,
                DataFechamentoAcordo = resposta.DataFechamentoAcordo,
                TotalParcelas = resposta.TotalParcelasAcordo,
                DataVencimento = resposta.DataVencimentoParcela,
                ValorParcela = resposta.ValorParcela,
                SaldoDevedorTotal = resposta.SaldoDevedorTotal,
                Produto = resposta.Produto,
                DescricaoProduto = resposta.DescricaoProduto,
                CodigoControleCliente = resposta.CodigoControleCliente,
                NossoNumero = resposta.NossoNumero,
                DataPagamento = resposta.DataPagamento,
                DataBaixa = dataBaixa,
                ValorPago = resposta.ValorPago,
                TipoPagamento = resposta.TipoPagamento,
                PeriodoOutros = resposta.ObterPeriodoOutros()
            };

            try
            {
                var valorParcelaAcordo = _parcelasAcordoService.ObterValorParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo);

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
                                                                         Convert.ToDateTime(arquivo.DataPagamento != null ? DateTime.ParseExact(arquivo.DataPagamento.ToString(), "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                                                                         arquivo.DataBaixa,
                                                                         arquivo.ValorPago,
                                                                         null);

                    var valorParcela = _parcelasAcordoService.ObterValorParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo);

                    await _acordoCobrancaService.AtualizarSaldoDevedor(arquivo.NumeroAcordo, (valorParcela ?? 0) * -1);

                    if (arquivo.Parcela == 1)
                    {
                        try
                        {
                            await _parcelasAcordoService.QuitarParcelasAcordo(numeroAcordo: arquivo.NumeroAcordo,
                                                                        matricula: arquivo.Matricula,
                                                                        sistema: arquivo.Sistema,
                                                                        dataPagamento: Convert.ToDateTime(arquivo.DataPagamento != null ? DateTime.ParseExact(arquivo.DataPagamento.ToString(), "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                                                                        periodo: arquivo.Periodo,
                                                                        idTitulo: arquivo.IdTitulo,
                                                                        codigoAtividade: arquivo.CodigoAtividade,
                                                                        numeroEvt: arquivo.NumeroEvt,
                                                                        idPessoa: arquivo.IdPessoa,
                                                                        codigobanco: arquivo.CodigoBanco,
                                                                        codigoAgencia: arquivo.CodigoAgencia,
                                                                        numeroConta: arquivo.NumeroConta,
                                                                        numeroCheque: arquivo.NumeroCheque,
                                                                        CpfCnpj: arquivo.CPF.ToString()
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

                var idErroLayout = await _arquivolayoutService.RegistrarErro(arquivo.DataBaixa, JsonSerializer.Serialize(resposta), ex.Erro, ex.Message);

                codErro = (int)ex.Erro;

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
                                                  Convert.ToDateTime(arquivo.DataPagamento != null ? DateTime.ParseExact(arquivo.DataPagamento.ToString(), "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                                                  arquivo.ValorPago,
                                                  codErro,
                                                  arquivo.CnpjEmpresaCobranca.ToString(),
                                                  arquivo.Sistema,
                                                  arquivo.SituacaoAluno,
                                                  arquivo.TipoInadimplencia,
                                                  arquivo.TipoPagamento);
        }

    }
}
