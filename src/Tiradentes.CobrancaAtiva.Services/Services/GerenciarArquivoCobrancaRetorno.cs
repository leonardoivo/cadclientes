using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Enum;
using Tiradentes.CobrancaAtiva.Domain.Exeptions;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class GerenciarArquivoCobrancaRetorno : IGerenciarArquivoCobrancaRetornoService
    {
        readonly IErroLayoutService _erroLayoutService;
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

        private Dictionary<int, decimal> Erros { get; set; }
        public GerenciarArquivoCobrancaRetorno(IErroLayoutService erroLayoutService,
                                               IParcelasAcordoService parcelasAcordoService,
                                               IAcordoCobrancaService acordoCobrancaService,
                                               IItensBaixasTipo1Service itensBaixasTipo1Service,
                                               IItensBaixasTipo2Service itensBaixasTipo2Service,
                                               IItensBaixasTipo3Service itensBaixasTipo3Service,
                                               IMatriculaAlunoExisteService matriculaAlunoExisteService,
                                               IItensGeracaoService itensGeracaoService,
                                               IParcelaTituloService parcelaTituloService,
                                               IParcelaPagaAlunoInstituicaoService parcelaPagaAlunoInstituicaoService,
                                               IBaixasCobrancasService baixasCobrancasService)
        {
            _erroLayoutService = erroLayoutService;            
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

            Erros = new Dictionary<int, decimal>();
        }


        private void ProcessaBaixaTipo1(dynamic arquivo, ref Dictionary<int, decimal> erros)
        {
            decimal? idErroLayout = null;

            try
            {

                if(_parcelasAcordoService.ExisteParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.ParcelaJaCadastrada);
                }                

                if(!_acordoCobrancaService.ExisteAcordo(arquivo.NumeroAcordo))
                {
                    _acordoCobrancaService.InserirAcordoCobranca(arquivo.NumeroAcordo,
                                                                    arquivo.DataBaixa, //?
                                                                    arquivo.DataFechamentoAcordo,
                                                                    arquivo.TotalParcelas,
                                                                    arquivo.ValorTotal,
                                                                    arquivo.Multa,
                                                                    arquivo.Matricula,
                                                                    arquivo.SaldoDevedorTotal);//? verificar se esta preenchido
                }

                if(arquivo.Parcela != 1)
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.AcordoNaoCadastrado);
                }

                //(Se o acordo existe e é a primeira parcela) ou (se existe acordo)
                //Apenas para a 1º parcelas ou para todas?
                _parcelasAcordoService.InserirPagamentoParcelaAcordo(arquivo.Parcela,
                                                                        arquivo.NumeroAcordo,
                                                                        arquivo.DataVencimento,
                                                                        arquivo.DataBaixa,
                                                                        arquivo.ValorPago);
            }
            catch (ErroArquivoCobrancaException ex)
            {

                idErroLayout = _erroLayoutService.RegistrarErro(ex.Erro);

                erros.Add(1, idErroLayout ?? 0);
            }


            _itensBaixasTipo1Service.InserirBaixa(arquivo.DataBaixa,
                                                    arquivo.Matricula,
                                                    arquivo.NumeroAcordo,
                                                    arquivo.Multa,
                                                    arquivo.Juros,
                                                    arquivo.DataVencimento,
                                                    arquivo.ValorParcela,
                                                    idErroLayout
                                                    );


        }

        private void ProcessaBaixaTipo2(dynamic arquivo, ref Dictionary<int, decimal> erros)
        {
            decimal? idErroLayout = null;

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

                if(dataEnvio != arquivo.DataVencimento)
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.DataInconsistente);
                }

                if(_parcelaTituloService.ExisteParcela(arquivo.Matricula, arquivo.Periodo, arquivo.Parcela))
                {
                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.ParcelaEnviadaAnteriormentePelaEmpresaCobranca);
                }

                if(_parcelaPagaAlunoInstituicao.ParcelaPagaInstituicao())
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
                if(!_acordoCobrancaService.ExisteAcordo(arquivo.NumeroAcordo))
                {
                    _acordoCobrancaService.AtualizarMatriculaAcordo(arquivo.Matricula, arquivo.NumeroAcordo);

                    _itensBaixasTipo1Service.AtualizarMatricula(arquivo.DataBaixa, arquivo.NumeroAcordo, arquivo.Matricula);
                }

                //Doc fala apenas se não deu erro, fluxograma diz sem validação.
                _parcelaTituloService.InserirParcela(arquivo.NumeroAcordo,
                                                     arquivo.Matricula,
                                                     arquivo.Periodo,
                                                     arquivo.Parcela,
                                                     arquivo.DataBaixa,
                                                     arquivo.DataEnvio,
                                                     arquivo.DataVencimento,
                                                     arquivo.ValorParcela);

                if(_parcelasAcordoService.ExisteParcelaPaga(arquivo.NumeroAcordo))
                {
                    //Ainda não implementado
                    _parcelasAcordoService.QuitarParcelasAcordo(arquivo.NumeroAcordo);
                }
            }
            catch (ErroArquivoCobrancaException ex)
            {

                idErroLayout = _erroLayoutService.RegistrarErro(ex.Erro);

                erros.Add(2, idErroLayout ?? 0);
            }


            _itensBaixasTipo2Service.InserirBaixa(arquivo.DataBaixa,
                                                    arquivo.Matricula,
                                                    arquivo.NumeroAcordo,
                                                    arquivo.Parcela,
                                                    arquivo.Periodo,
                                                    arquivo.DataVencimento,
                                                    arquivo.Valor,
                                                    idErroLayout);
        }
        private void ProcessaBaixaTipo3(dynamic arquivo, ref Dictionary<int, decimal> erros)
        {
            decimal? idErroLayout = null;

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

                    _parcelasAcordoService.InserirPagamentoParcelaAcordo(arquivo.Parcela,
                                                                         arquivo.NumeroAcordo,
                                                                         arquivo.DataVencimento,
                                                                         arquivo.DataBaixa,
                                                                         arquivo.ValorPago);

                    var saldoDecremendo = _parcelasAcordoService.ObterValorParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo);

                    _acordoCobrancaService.AtualizarSaldoDevedor(arquivo.NumeroAcordo, saldoDecremendo ?? 0 * -1);

                    if(arquivo.Parcela == 1)
                    {
                        try
                        {
                            _parcelasAcordoService.QuitarParcelasAcordo(arquivo.NumeroAcordo);
                        }
                        catch (Exception)
                        {

                            var saldoAdicao = _parcelasAcordoService.ObterValorParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo);

                            _acordoCobrancaService.AtualizarSaldoDevedor(arquivo.NumeroAcordo, saldoAdicao);

                            throw;
                        }
                    }
                }
                catch (Exception)
                {

                    _parcelasAcordoService.EstornarParcelaAcordo(arquivo.Parcela, arquivo.NumeroAcordo);

                    throw new ErroArquivoCobrancaException(ErrosBaixaPagamento.OutrosErros);
                }
            }
            catch (ErroArquivoCobrancaException ex)
            {

                idErroLayout = _erroLayoutService.RegistrarErro(ex.Erro);

                erros.Add(3, idErroLayout ?? 0);
            }

            _itensBaixasTipo3Service.InserirBaixa(arquivo.DataBaixa,
                                                  arquivo.Matricula,
                                                  arquivo.NumeroAcordo,
                                                  arquivo.Parcela,
                                                  arquivo.DataPagamento,
                                                  arquivo.ValorPago,
                                                  idErroLayout);
        }

        public void Gerenciar(IEnumerable<dynamic> arquivos)
        {
            Dictionary<int, decimal> ErrosContabilizados = new Dictionary<int, decimal>();

            foreach (var arquivo in arquivos)
            {

                if(arquivo.Tipo == "ArquivoTipo1")
                {
                    ProcessaBaixaTipo1(arquivo, ref ErrosContabilizados);
                }
                else if(arquivo.Tipo == "ArquivoTipo2")
                {
                    ProcessaBaixaTipo2(arquivo, ref ErrosContabilizados);
                }
                else if(arquivo.Tipo == "ArquivoTipo3")
                {
                    ProcessaBaixaTipo3(arquivo, ref ErrosContabilizados);
                }
                else
                {
                    //Preciso tratar os erros adequadamente
                    _erroLayoutService.RegistrarErro(ErrosBaixaPagamento.LayoutInconsistente);
                }
            }

            _baixasCobrancasService.AtualizarBaixasCobrancas();
        }
    }
}
/*
        public string TipoRegistro { get; set; }
        public string CPF { get; set; }
        public string NumeroAcordo { get; set; }
        public string Parcela { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
        public string ChaveInadimplencia { get; set; }

        //tipo 1
        public string Juros { get; set; }
        public string Multa { get; set; }
        public string ValorTotal { get; set; }
        public string DataFechamentoAcordo { get; set; }
        public string TotalParcelas { get; set; }

        //tipo 1 e tipo 2
        public string DataVencimento { get; set; }
        public string ValorParcela { get; set; }

        //tipo 2
        public string SaldoDevedorTotal { get; set; }
        public string Produto { get; set; }
        public string DescricaoProduto { get; set; }
        public string Fase { get; set; }
        public string CodigoControleCliente { get; set; }

        //tipo 3
        public string NossoNumero { get; set; }
        public string DataPagamento { get; set; }
        public string DataBaixa { get; set; }
        public string ValorPago { get; set; }
        public string TipoPagamento { get; set; }

 */