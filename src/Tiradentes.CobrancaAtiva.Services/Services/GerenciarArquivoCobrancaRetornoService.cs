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
            int? codErro = null;

            var arquivo = new
            {
                TipoRegistro = resposta.TipoRegistro,
                CPF = resposta.CPF,
                NumeroAcordo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.NumeroAcordo) ? resposta.NumeroAcordo : "0"),
                Parcela = Convert.ToInt32(!string.IsNullOrEmpty(resposta.Parcela) ? resposta.Parcela : "0"),
                CnpjEmpresaCobranca = resposta.CnpjEmpresaCobranca,
                SituacaoAluno = resposta.SituacaoAluno,
                Sistema = resposta.Sistema,
                Matricula = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Matricula) ? resposta.Matricula : "0"),
                Periodo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Periodo) ? resposta.Periodo : "0"),
                IdTitulo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdTitulo) ? resposta.IdTitulo : "0"),
                CodigoAtividade = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAtividade) ? resposta.CodigoAtividade : "0"),
                NumeroEvt = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroEvt) ? resposta.NumeroEvt : "0"),
                IdPessoa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdPessoa) ? resposta.IdPessoa : "0"),
                CodigoBanco = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoBanco) ? resposta.CodigoBanco : "0"),
                CodigoAgencia = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAgencia) ? resposta.CodigoAgencia : "0"),
                NumeroConta = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroConta) ? resposta.NumeroConta : "0"),
                NumeroCheque = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.NumeroCheque) ? resposta.NumeroCheque : "0"),
                TipoInadimplencia = resposta.TipoInadimplencia,
                ChaveInadimplencia = resposta.ChaveInadimplencia,
                Juros = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Juros) ? resposta.Juros : "0"),
                Multa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Multa) ? resposta.Multa : "0"),
                ValorTotal = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorTotal) ? resposta.ValorTotal : "0"),
                DataFechamentoAcordo = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataFechamentoAcordo) ? DateTime.ParseExact(resposta.DataFechamentoAcordo, "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                TotalParcelas = Convert.ToInt32(!string.IsNullOrEmpty(resposta.TotalParcelas) ? resposta.TotalParcelas : "0"),
                DataVencimento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataVencimento) ? DateTime.ParseExact(resposta.DataVencimento, "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                ValorParcela = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorParcela) ? resposta.ValorParcela : "0"),
                SaldoDevedorTotal = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.SaldoDevedorTotal) ? resposta.SaldoDevedorTotal : "0"),
                Produto = resposta.Produto,
                DescricaoProduto = resposta.DescricaoProduto,
                Fase = resposta.Fase,
                CodigoControleCliente = resposta.CodigoControleCliente,
                NossoNumero = resposta.NossoNumero,
                DataPagamento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataPagamento) ? DateTime.ParseExact(resposta.DataPagamento, "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                DataBaixa = dataBaixa,
                ValorPago = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorPago) ? resposta.ValorPago : "0"),
                TipoPagamento = resposta.TipoPagamento
            };

            try
            {

                if(_parcelasAcordoService.ExisteParcelaAcordo(Convert.ToDecimal(arquivo.Parcela), Convert.ToDecimal(arquivo.NumeroAcordo)))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.ParcelaJaCadastrada);
                }                

                if(!_acordoCobrancaService.ExisteAcordo(Convert.ToDecimal(arquivo.NumeroAcordo)))
                {
                    await _acordoCobrancaService.InserirAcordoCobranca(arquivo.NumeroAcordo,
                                                                       arquivo.DataBaixa, //?
                                                                       arquivo.DataFechamentoAcordo,
                                                                       arquivo.TotalParcelas,
                                                                       arquivo.ValorTotal,
                                                                       arquivo.Multa,
                                                                       arquivo.Matricula,
                                                                       arquivo.SaldoDevedorTotal, //? verificar se esta preenchido
                                                                       arquivo.CPF,
                                                                       arquivo.CnpjEmpresaCobranca,
                                                                       arquivo.Sistema,
                                                                       arquivo.TipoInadimplencia);
                }

                //if(Convert.ToInt32(arquivo.Parcela) != 1)
                //{
                //    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.AcordoNaoCadastrado);
                //}

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
                    CodErro = codErro ?? 0
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
            int? codErro = null;

            var arquivo = new
            {
                TipoRegistro = resposta.TipoRegistro,
                CPF = resposta.CPF,
                NumeroAcordo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.NumeroAcordo) ? resposta.NumeroAcordo : "0"),
                Parcela = Convert.ToInt32(!string.IsNullOrEmpty(resposta.Parcela) ? resposta.Parcela : "0"),
                CnpjEmpresaCobranca = resposta.CnpjEmpresaCobranca,
                SituacaoAluno = resposta.SituacaoAluno,
                Sistema = resposta.Sistema,
                Matricula = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Matricula) ? resposta.Matricula : "0"),
                Periodo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Periodo) ? resposta.Periodo : "0"),
                IdTitulo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdTitulo) ? resposta.IdTitulo : "0"),
                CodigoAtividade = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAtividade) ? resposta.CodigoAtividade : "0"),
                NumeroEvt = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroEvt) ? resposta.NumeroEvt : "0"),
                IdPessoa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdPessoa) ? resposta.IdPessoa : "0"),
                CodigoBanco = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoBanco) ? resposta.CodigoBanco : "0"),
                CodigoAgencia = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAgencia) ? resposta.CodigoAgencia : "0"),
                NumeroConta = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroConta) ? resposta.NumeroConta : "0"),
                NumeroCheque = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.NumeroCheque) ? resposta.NumeroCheque : "0"),
                TipoInadimplencia = resposta.TipoInadimplencia,
                ChaveInadimplencia = resposta.ChaveInadimplencia,
                Juros = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Juros) ? resposta.Juros : "0"),
                Multa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Multa) ? resposta.Multa : "0"),
                ValorTotal = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorTotal) ? resposta.ValorTotal : "0"),
                DataFechamentoAcordo = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataFechamentoAcordo) ? DateTime.ParseExact(resposta.DataFechamentoAcordo, "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                TotalParcelas = Convert.ToInt32(!string.IsNullOrEmpty(resposta.TotalParcelas) ? resposta.TotalParcelas : "0"),
                DataVencimento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataVencimento) ? DateTime.ParseExact(resposta.DataVencimento, "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                ValorParcela = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorParcela) ? resposta.ValorParcela : "0"),
                SaldoDevedorTotal = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.SaldoDevedorTotal) ? resposta.SaldoDevedorTotal : "0"),
                Produto = resposta.Produto,
                DescricaoProduto = resposta.DescricaoProduto,
                Fase = resposta.Fase,
                CodigoControleCliente = resposta.CodigoControleCliente,
                NossoNumero = resposta.NossoNumero,
                DataPagamento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataPagamento) ? DateTime.ParseExact(resposta.DataPagamento, "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                DataBaixa = dataBaixa,
                ValorPago = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorPago) ? resposta.ValorPago : "0"),
                TipoPagamento = resposta.TipoPagamento
            };

            try
            {

                if (!_matriculaAlunoExisteService.MatriculaAlunoExiste(arquivo.TipoInadimplencia,
                                                                       arquivo.Sistema,
                                                                       arquivo.Matricula))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.MatriculaInexistente);
                }

                var dataEnvio =_itensGeracaoService.ObterDataEnvio(arquivo.CnpjEmpresaCobranca,
                                                                   arquivo.Matricula,
                                                                   arquivo.Periodo,
                                                                   arquivo.Parcela);

                if(dataEnvio != Convert.ToDateTime(arquivo.DataVencimento))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.DataInconsistente);
                }

                if(_parcelaTituloService.ExisteParcela(arquivo.Matricula, arquivo.Periodo, arquivo.Parcela))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.ParcelaEnviadaAnteriormentePelaEmpresaCobranca);
                }

                if(_parcelaPagaAlunoInstituicao.ParcelaPagaInstituicao(tipoInadimplencia: arquivo.TipoInadimplencia,
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

                if(_itensGeracaoService.ExisteMatricula(arquivo.CnpjEmpresaCobranca,
                                                        arquivo.Matricula,
                                                        arquivo.Periodo,
                                                        arquivo.Parcela))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.GeracaoInconsistente);
                }

                //Doc fala Se existe, fluxograma fala Se Não existe
                if(!_acordoCobrancaService.ExisteAcordo(Convert.ToDecimal(arquivo.NumeroAcordo)))
                {
                    await _acordoCobrancaService.AtualizarMatriculaAcordo(arquivo.Matricula, arquivo.NumeroAcordo);

                    await _itensBaixasTipo1Service.AtualizarMatricula(arquivo.DataBaixa, Convert.ToDecimal(arquivo.NumeroAcordo), arquivo.Matricula);
                }

                //Doc fala apenas se não deu erro, fluxograma diz sem validação.
                await _parcelaTituloService.InserirParcela(arquivo.NumeroAcordo,
                                                     arquivo.Matricula,
                                                     arquivo.Periodo,
                                                     arquivo.Parcela,
                                                     arquivo.DataBaixa,
                                                     dataEnvio,
                                                     arquivo.DataVencimento,
                                                     arquivo.ValorParcela);

                if(_parcelasAcordoService.ExisteParcelaPaga(Convert.ToDecimal(arquivo.NumeroAcordo)))
                {
                    //Ainda não implementado
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
                    CodErro = codErro ?? 0
                });
            }


            await _itensBaixasTipo2Service.InserirBaixa(arquivo.DataBaixa,
                                                  arquivo.Matricula,
                                                  arquivo.NumeroAcordo,
                                                  arquivo.Parcela,
                                                  arquivo.Periodo,
                                                  arquivo.DataVencimento,
                                                  arquivo.ValorPago,
                                                  codErro);
        }

        private async Task ProcessaBaixaTipo3(DateTime dataBaixa, RespostaViewModel resposta,  List<ErroParcelaViewModel> erros)
        {
            int? codErro = null;

            var arquivo = new
            {
                TipoRegistro = resposta.TipoRegistro,
                CPF = resposta.CPF,
                NumeroAcordo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.NumeroAcordo) ? resposta.NumeroAcordo : "0"),
                Parcela = Convert.ToInt32(!string.IsNullOrEmpty(resposta.Parcela) ? resposta.Parcela : "0"),
                CnpjEmpresaCobranca = resposta.CnpjEmpresaCobranca,
                SituacaoAluno = resposta.SituacaoAluno,
                Sistema = resposta.Sistema,
                Matricula = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Matricula) ? resposta.Matricula : "0"),
                Periodo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Periodo) ? resposta.Periodo : "0"),
                IdTitulo = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdTitulo) ? resposta.IdTitulo : "0"),
                CodigoAtividade = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAtividade) ? resposta.CodigoAtividade : "0"),
                NumeroEvt = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroEvt) ? resposta.NumeroEvt : "0"),
                IdPessoa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.IdPessoa) ? resposta.IdPessoa : "0"),
                CodigoBanco = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoBanco) ? resposta.CodigoBanco : "0"),
                CodigoAgencia = Convert.ToInt32(!string.IsNullOrEmpty(resposta.CodigoAgencia) ? resposta.CodigoAgencia : "0"),
                NumeroConta = Convert.ToInt32(!string.IsNullOrEmpty(resposta.NumeroConta) ? resposta.NumeroConta : "0"),
                NumeroCheque = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.NumeroCheque) ? resposta.NumeroCheque : "0"),
                TipoInadimplencia = resposta.TipoInadimplencia,
                ChaveInadimplencia = resposta.ChaveInadimplencia,
                Juros = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Juros) ? resposta.Juros : "0"),
                Multa = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.Multa) ? resposta.Multa : "0"),
                ValorTotal = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorTotal) ? resposta.ValorTotal : "0"),
                DataFechamentoAcordo = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataFechamentoAcordo) ? DateTime.ParseExact(resposta.DataFechamentoAcordo, "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                TotalParcelas = Convert.ToInt32(!string.IsNullOrEmpty(resposta.TotalParcelas) ? resposta.TotalParcelas : "0"),
                DataVencimento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataVencimento) ? DateTime.ParseExact(resposta.DataVencimento, "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                ValorParcela = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorParcela) ? resposta.ValorParcela : "0"),
                SaldoDevedorTotal = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.SaldoDevedorTotal) ? resposta.SaldoDevedorTotal : "0"),
                Produto = resposta.Produto,
                DescricaoProduto = resposta.DescricaoProduto,
                Fase = resposta.Fase,
                CodigoControleCliente = resposta.CodigoControleCliente,
                NossoNumero = resposta.NossoNumero,
                DataPagamento = Convert.ToDateTime(!string.IsNullOrEmpty(resposta.DataPagamento) ? DateTime.ParseExact(resposta.DataPagamento, "ddMMyyyy", CultureInfo.InvariantCulture) : "01-01-0001"),
                DataBaixa = dataBaixa,
                ValorPago = Convert.ToDecimal(!string.IsNullOrEmpty(resposta.ValorPago) ? resposta.ValorPago : "0"),
                TipoPagamento = resposta.TipoPagamento
            };

            try
            {
                var valorParcelaAcordo = _parcelasAcordoService.ObterValorParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo);

                if(valorParcelaAcordo == null)
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.AcordoNaoCadastrado);
                }

                if(arquivo.ValorPago < valorParcelaAcordo)
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.ValorPagoInsuficiente);
                }

                if(_parcelasAcordoService.ParcelaPaga(arquivo.Parcela, arquivo.NumeroAcordo))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.ParcelaAcordoJaPaga);
                }

                if(!_parcelaTituloService.ExisteParcelaInadimplente(arquivo.DataBaixa))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.NaoExisteParcelasVinculadas);
                }

                try
                {

                    await _parcelasAcordoService.InserirPagamentoParcelaAcordo(arquivo.Parcela,
                                                                         arquivo.NumeroAcordo,
                                                                         arquivo.Sistema,
                                                                         arquivo.DataBaixa,
                                                                         arquivo.DataVencimento,                                                                         
                                                                         arquivo.ValorParcela,
                                                                         arquivo.CnpjEmpresaCobranca,
                                                                         arquivo.TipoInadimplencia);

                    var saldoDecremendo = _parcelasAcordoService.ObterValorParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo);

                    await _acordoCobrancaService.AtualizarSaldoDevedor(arquivo.NumeroAcordo, saldoDecremendo ?? 0 * -1);

                    if(arquivo.Parcela == 1)
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

                            var saldoAdicao = _parcelasAcordoService.ObterValorParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo);

                            await _acordoCobrancaService.AtualizarSaldoDevedor(arquivo.NumeroAcordo, saldoAdicao ?? 0);

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
                    CodErro = codErro ?? 0
                });
            }

            await _itensBaixasTipo3Service.InserirBaixa(arquivo.DataBaixa,
                                                  arquivo.Matricula,
                                                  arquivo.NumeroAcordo,
                                                  arquivo.Parcela,
                                                  arquivo.DataPagamento,
                                                  arquivo.ValorPago,
                                                  codErro);
        }

        public async Task Gerenciar()
        {

            DateTime DataBaixa = DateTime.MinValue;

            try
            {



                List<ErroParcelaViewModel> ErrosContabilizados = new List<ErroParcelaViewModel>();


                var arquivos = _cobrancaService.BuscarRepostaNaoIntegrada().Result;

                try
                {

                    DataBaixa = await _arquivolayoutService.SalvarLayoutArquivo("S", JsonSerializer.Serialize(arquivos));
                }
                catch (Exception ex)
                {

                    throw new ArgumentNullException("Arquivo Layout existente", ex);
                }

                var model = await _baixasCobrancasService.Buscar(DataBaixa);

                if(model == null)
                {
                    await _baixasCobrancasService.CriarBaixasCobrancas(DataBaixa);
                }

                foreach (var arquivo in arquivos)
                {
                    try
                    {

                        if(arquivo.TipoRegistro == "1")
                        {
                            await ProcessaBaixaTipo1(DataBaixa, arquivo, ErrosContabilizados);
                        }
                        else if(arquivo.TipoRegistro == "2")
                        {
                            await ProcessaBaixaTipo2(DataBaixa, arquivo, ErrosContabilizados);
                        }
                        else if(arquivo.TipoRegistro == "3")
                        {
                            await ProcessaBaixaTipo3(DataBaixa, arquivo, ErrosContabilizados);
                        }
                        else
                        {
                            await _arquivolayoutService.RegistrarErro(DataBaixa, JsonSerializer.Serialize(arquivo), ErrosBaixaPagamento.ErroInternoServidor, "");
                        }
                    }
                    catch (Exception ex)
                    {

                        await _arquivolayoutService.RegistrarErro(DataBaixa, JsonSerializer.Serialize(ex.StackTrace), ErrosBaixaPagamento.ErroInternoServidor, ex.Message);
                    }
                }

                await _baixasCobrancasService.AtualizarBaixasCobrancas(new BaixasCobrancasViewModel() {
                            DataBaixa = DataBaixa,
                            Etapa = 3,
                            QuantidadeTipo1 = arquivos.Count(A => A.TipoRegistro == "1"),
                            QuantidadeTipo2 = arquivos.Count(A => A.TipoRegistro == "2"),
                            QuantidadeTipo3 = arquivos.Count(A => A.TipoRegistro == "3"),

                            ValorTotalTipo1 = arquivos.Where(A => A.TipoRegistro == "1").Sum(A => Convert.ToDecimal(A.ValorParcela)),
                            ValorTotalTipo2 = arquivos.Where(A => A.TipoRegistro == "2").Sum(A => Convert.ToDecimal(A.ValorParcela)),
                            ValorTotalTipo3 = arquivos.Where(A => A.TipoRegistro == "3").Sum(A => Convert.ToDecimal(A.ValorParcela)),

                            QuantidadeErrosTipo1 = ErrosContabilizados.Count(E => E.Etapa == 1),
                            QuantidadeErrosTipo2 = ErrosContabilizados.Count(E => E.Etapa == 2),
                            QuantidadeErrosTipo3 = ErrosContabilizados.Count(E => E.Etapa == 3),

                            ValorTotalErrosTipo1 = ErrosContabilizados.Where(E => E.Etapa == 1).Sum(E => E.ValorParcela),
                            ValorTotalErrosTipo2 = ErrosContabilizados.Where(E => E.Etapa == 2).Sum(E => E.ValorParcela),
                            ValorTotalErrosTipo3 = ErrosContabilizados.Where(E => E.Etapa == 3).Sum(E => E.ValorParcela),
                            UserName = ""
                        });
            }
            catch(ArgumentNullException ex)
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
