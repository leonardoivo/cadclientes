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
                Juros = resposta.Juros,
                Multa = resposta.Multa,
                ValorTotal = resposta.ValorTotal,
                DataFechamentoAcordo = resposta.DataFechamentoAcordo,
                TotalParcelas = resposta.TotalParcelas,
                DataVencimento = resposta.DataVencimento,
                ValorParcela = resposta.ValorParcela,
                SaldoDevedorTotal = resposta.SaldoDevedorTotal,
                Produto = resposta.Produto,
                DescricaoProduto = resposta.DescricaoProduto,
                Fase = resposta.Fase,
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
                                                  arquivo.CnpjEmpresaCobranca,
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
                Juros = resposta.Juros,
                Multa = resposta.Multa,
                ValorTotal = resposta.ValorTotal,
                DataFechamentoAcordo = resposta.DataFechamentoAcordo,
                TotalParcelas = resposta.TotalParcelas,
                DataVencimento = resposta.DataVencimento,
                ValorParcela = resposta.ValorParcela,
                SaldoDevedorTotal = resposta.SaldoDevedorTotal,
                Produto = resposta.Produto,
                DescricaoProduto = resposta.DescricaoProduto,
                Fase = resposta.Fase,
                CodigoControleCliente = resposta.CodigoControleCliente,
                NossoNumero = resposta.NossoNumero,
                DataPagamento = resposta.DataPagamento,
                DataBaixa = dataBaixa,
                ValorPago = resposta.ValorPago,
                TipoPagamento = resposta.TipoPagamento,
                PeriodoChequeDevolvido = $"{resposta.CodigoBanco}#{resposta.CodigoAgencia}#{resposta.NumeroConta}#{resposta.NumeroCheque}"
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
                                                  arquivo.CnpjEmpresaCobranca,
                                                  arquivo.Sistema,
                                                  arquivo.SituacaoAluno,
                                                  arquivo.TipoInadimplencia,
                                                  arquivo.PeriodoChequeDevolvido);
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
                Juros = resposta.Juros,
                Multa = resposta.Multa,
                ValorTotal = resposta.ValorTotal,
                DataFechamentoAcordo = resposta.DataFechamentoAcordo,
                TotalParcelas = resposta.TotalParcelas,
                DataVencimento = resposta.DataVencimento,
                ValorParcela = resposta.ValorParcela,
                SaldoDevedorTotal = resposta.SaldoDevedorTotal,
                Produto = resposta.Produto,
                DescricaoProduto = resposta.DescricaoProduto,
                Fase = resposta.Fase,
                CodigoControleCliente = resposta.CodigoControleCliente,
                NossoNumero = resposta.NossoNumero,
                DataPagamento = resposta.DataPagamento,
                DataBaixa = dataBaixa,
                ValorPago = resposta.ValorPago,
                TipoPagamento = resposta.TipoPagamento
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
                                                                         arquivo.DataPagamento,
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
                                                  arquivo.DataPagamento,
                                                  arquivo.ValorPago,
                                                  codErro,
                                                  arquivo.CnpjEmpresaCobranca,
                                                  arquivo.Sistema,
                                                  arquivo.SituacaoAluno,
                                                  arquivo.TipoInadimplencia,
                                                  arquivo.TipoPagamento);
        }

        public async Task Gerenciar()
        {
            DateTime DataBaixa = DateTime.MinValue;

            try
            {
                List<ErroParcelaViewModel> ErrosContabilizados = new List<ErroParcelaViewModel>();

                var arquivos = _cobrancaService.BuscarRepostaNaoIntegrada().Result;

                if (arquivos.Count() == 0)
                    return;

                try
                {
                    DataBaixa = await _arquivolayoutService.SalvarLayoutArquivo("S", JsonSerializer.Serialize(arquivos));
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException("Arquivo Layout existente", ex);
                }

                var model = await _baixasCobrancasService.Buscar(DataBaixa);

                if (model == null)
                {
                    await _baixasCobrancasService.CriarBaixasCobrancas(DataBaixa);
                }

                foreach (var arquivo in arquivos.OrderBy(A => A.TipoRegistro).ThenBy(A => A.Parcela))
                {
                    try
                    {
                        if (arquivo.TipoRegistro == 1)
                        {
                            await ProcessaBaixaTipo1(DataBaixa, arquivo, ErrosContabilizados);
                        }
                        else if (arquivo.TipoRegistro == 2)
                        {
                            await ProcessaBaixaTipo2(DataBaixa, arquivo, ErrosContabilizados);
                        }
                        else if (arquivo.TipoRegistro == 3)
                        {
                            await ProcessaBaixaTipo3(DataBaixa, arquivo, ErrosContabilizados);
                        }
                        else
                        {
                            await _arquivolayoutService.RegistrarErro(DataBaixa, JsonSerializer.Serialize(arquivo), ErrosBaixaPagamento.ErroInternoServidor, "");
                        }

                        arquivo.Integrado = true;
                        _cobrancaService.AlterarStatus(arquivo);
                    }
                    catch (Exception ex)
                    {
                        await _arquivolayoutService.RegistrarErro(DataBaixa, JsonSerializer.Serialize(ex.StackTrace), ErrosBaixaPagamento.ErroInternoServidor, ex.Message);
                    }
                }

                await _baixasCobrancasService.AtualizarBaixasCobrancas(new BaixasCobrancasViewModel()
                {
                    DataBaixa = DataBaixa,
                    Etapa = 3,
                    QuantidadeTipo1 = arquivos.Count(A => A.TipoRegistro == 1),
                    QuantidadeTipo2 = arquivos.Count(A => A.TipoRegistro == 2),
                    QuantidadeTipo3 = arquivos.Count(A => A.TipoRegistro == 3),

                    ValorTotalTipo1 = arquivos.Where(A => A.TipoRegistro == 1).Sum(A => A.ValorParcela),
                    ValorTotalTipo2 = arquivos.Where(A => A.TipoRegistro == 2).Sum(A => A.ValorParcela),
                    ValorTotalTipo3 = arquivos.Where(A => A.TipoRegistro == 3).Sum(A => A.ValorParcela),

                    QuantidadeErrosTipo1 = ErrosContabilizados.Count(E => E.Etapa == 1),
                    QuantidadeErrosTipo2 = ErrosContabilizados.Count(E => E.Etapa == 2),
                    QuantidadeErrosTipo3 = ErrosContabilizados.Count(E => E.Etapa == 3),

                    ValorTotalErrosTipo1 = ErrosContabilizados.Where(E => E.Etapa == 1).Sum(E => E.ValorParcela),
                    ValorTotalErrosTipo2 = ErrosContabilizados.Where(E => E.Etapa == 2).Sum(E => E.ValorParcela),
                    ValorTotalErrosTipo3 = ErrosContabilizados.Where(E => E.Etapa == 3).Sum(E => E.ValorParcela),
                    UserName = ""
                });
            }
            catch (ArgumentNullException ex)
            {
                var dataErro = await _arquivolayoutService.SalvarLayoutArquivo("E", "Arquivo ja processado com a data de hoje");
                await _arquivolayoutService.RegistrarErro(dataErro, JsonSerializer.Serialize(ex.StackTrace), ErrosBaixaPagamento.OutrosErros, ex.Message);
            }
            catch (Exception ex)
            {
                await _arquivolayoutService.RegistrarErro(DataBaixa, JsonSerializer.Serialize(ex.StackTrace), ErrosBaixaPagamento.ErroInternoServidor, ex.Message);
            }
        }
    }
}
