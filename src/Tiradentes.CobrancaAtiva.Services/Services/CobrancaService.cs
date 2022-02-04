using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.Validations.RegularizacaoParcelasAcordo;
using Tiradentes.CobrancaAtiva.Application.Validations.RespostaCobranca;
using Tiradentes.CobrancaAtiva.Application.ViewModels.BaixaPagamento;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class CobrancaService : BaseService, ICobrancaService
    {
        protected readonly ICobrancaRepository _repositorio;
        protected readonly IAlunosInadimplentesRepository _alunosInadimplentesRepository;
        protected readonly IRegraNegociacaoService _regraNegociacaoService;
        protected readonly IInstituicaoService _instituicaoService;
        protected readonly IParcelasAcordoService _parcelaService;
        protected readonly IModalidadeService _modalidadeService;
        protected readonly ICursoService _cursoService;
        protected readonly IParcelasAcordoService _parcelasAcordoService;
        protected readonly IAcordoCobrancaService _acordoCobrancaService;
        protected readonly IMapper _map;

        public CobrancaService(
            ICobrancaRepository repositorio,
            IAlunosInadimplentesRepository alunosInadimplentesRepository,
            IRegraNegociacaoService regraNegociacaoService,
            IInstituicaoService instituicaoService,
            IParcelasAcordoService parcelaService,
            IModalidadeService modalidadeService,
            ICursoService cursoService,
            IParcelasAcordoService parcelasAcordoService,
            IAcordoCobrancaService acordoCobrancaService,
            IMapper map
        )
        {
            _repositorio = repositorio;
            _alunosInadimplentesRepository = alunosInadimplentesRepository;
            _regraNegociacaoService = regraNegociacaoService;
            _instituicaoService = instituicaoService;
            _parcelaService = parcelaService;
            _modalidadeService = modalidadeService;
            _cursoService = cursoService;
            _parcelasAcordoService = parcelasAcordoService;
            _acordoCobrancaService = acordoCobrancaService;
            _map = map;
        }

        public RespostaViewModel AlterarStatus(RespostaViewModel viewModel)
        {
            var model = _map.Map<RespostasCollection>(viewModel);

            var returnModel = _repositorio.AlterarStatus(model).Result;

            return _map.Map<RespostaViewModel>(returnModel);

        }

        public async Task<IEnumerable<RespostaViewModel>> BuscarRepostaNaoIntegrada()
        {
            var arquivosResposta = await _repositorio.Buscar(C => !C.Integrado);

            var viewModelList = from arq in arquivosResposta
                                select _map.Map<RespostaViewModel>(arq);

            return viewModelList;
        }

        public async Task BaixaManual(BaixaPagamentoParcelaManualViewModel viewModel)
        {
            await _parcelaService.AtualizaPagamentoParcelaAcordoBanco(viewModel);
        }

        public async Task<RespostaViewModel> Criar(RespostaViewModel viewModel)
        {
            Validate(new CriarRespostaCobrancaValidation(), viewModel);


            var model = _map.Map<RespostasCollection>(viewModel);

            await _repositorio.Criar(model);

            return _map.Map<RespostaViewModel>(model);
        }

        public async Task<RegularizarParcelasAcordoViewModel> RegularizarAcordoCobranca(RegularizarParcelasAcordoViewModel viewModel)
        {
            try
            {
                Validate(new RegularizacaoParcelasAcordoValidation(), viewModel);

                await _parcelasAcordoService.AtualizaPagamentoParcelaAcordo(viewModel.Parcela,
                                                                     viewModel.NumeroAcordo,
                                                                     viewModel.DataPagamento,
                                                                     viewModel.DataPagamento,
                                                                     viewModel.ValorPago,
                                                                     "R");


                await _acordoCobrancaService.AtualizarSaldoDevedor(viewModel.NumeroAcordo, viewModel.ValorPago * -1);

                if (viewModel.Parcela == 1)
                {
                    try
                    {
                        await _parcelasAcordoService.QuitarParcelasAcordo(numeroAcordo: viewModel.NumeroAcordo,
                                                                    matricula: viewModel.Matricula,
                                                                    sistema: viewModel.Sistema,
                                                                    dataPagamento: viewModel.DataPagamento,
                                                                    periodo: viewModel.ObterPeriodo(),
                                                                    idTitulo: viewModel.IdTitulo,
                                                                    codigoAtividade: viewModel.CodigoAtividade,
                                                                    numeroEvt: viewModel.NumeroEvt,
                                                                    idPessoa: viewModel.IdPessoa,
                                                                    codigobanco: viewModel.CodigoBanco,
                                                                    codigoAgencia: viewModel.CodigoAgencia,
                                                                    numeroConta: viewModel.NumeroConta,
                                                                    numeroCheque: viewModel.NumeroCheque,
                                                                    CpfCnpj: viewModel.CPF
                                                                    );

                        await _parcelasAcordoService.InserirObservacaoRegularizacaoParcelaAcordo(viewModel.CnpjEmpresaCobranca, viewModel.NumeroAcordo, viewModel.Parcela, viewModel.Texto);
                    }
                    catch (Exception)
                    {

                        await _acordoCobrancaService.AtualizarSaldoDevedor(viewModel.NumeroAcordo, viewModel.ValorPago);

                        throw;
                    }
                }
            }
            catch (Exception)
            {
                await _parcelasAcordoService.EstornarParcelaAcordo(viewModel.Parcela, viewModel.NumeroAcordo);
            }

            return viewModel;
        }

        public async Task<IEnumerable<string>> ListarFiltrosMatricula(string matricula)
        {
            var baixas = await _repositorio.ListarFiltroPorMatricula(matricula);

            return baixas.GroupBy(b => b.Matricula.ToString()).Select(b => b.Key);
        }

        public async Task<IEnumerable<string>> ListarFiltrosAcordo(string acordo)
        {
            var baixas = await _repositorio.ListarFiltroPorAcordo(acordo);

            return baixas.GroupBy(b => b.NumeroAcordo.ToString()).Select(b => b.Key);
        }

        public async Task<IEnumerable<string>> ListarFiltroCpf(string cpf)
        {
            var baixas = await _repositorio.ListarFiltroPorCpf(cpf);

            return baixas.GroupBy(b => b.CPF.ToString()).Select(b => b.Key);
        }

        public async Task<IEnumerable<string>> ListarFiltroNomeAluno(string nomeAluno)
        {
            var baixas = await _repositorio.ListarFiltroPorAluno(nomeAluno);

            return baixas.GroupBy(b => b.NomeAluno).Select(b => b.Key);
        }

        public async Task<ModelPaginada<BaixaPagamento>> Listar(ConsultaBaixaPagamentoQueryParam queryParam)
        {
            var resultadoBaixas = new List<BaixaPagamento>();

            var queryParams = _map.Map<BaixaPagamentoQueryParam>(queryParam);

            var baixas = await _repositorio.Listar(queryParams);

            var agrupadoPorAcordo = baixas.GroupBy(b => b.NumeroAcordo);

            foreach (var group in agrupadoPorAcordo)
            {
                var parcelaUniq = group.First();

                var instituicao = (await _instituicaoService.Buscar()).Where(i => i.Id == parcelaUniq.InstituicaoEnsino).First();
                var modalidade = await _modalidadeService.BuscarPorCodigo(parcelaUniq.Sistema);


                var baixaPagamento = new BaixaPagamento()
                {
                    DataBaixa = parcelaUniq.DataBaixa.ToString(),
                    DataNegociacao = parcelaUniq.DataFechamentoAcordo.ToString(),
                    EmpresaParceira = parcelaUniq.CnpjEmpresaCobranca.ToString(),
                    FormaPagamento = parcelaUniq.TipoPagamento,
                    InstituicaoEnsino = instituicao.Instituicao,
                    InstituicaoModel = new Domain.Models.InstituicaoModel()
                    {
                        Id = instituicao.Id,
                        Instituicao = instituicao.Instituicao
                    },
                    Matricula = parcelaUniq.Matricula.ToString(),
                    ModalidadeEnsino = modalidade.Modalidade,
                    ModalidadeModel = new Domain.Models.ModalidadeModel()
                    {
                        Id = modalidade.Id,
                        Modalidade = modalidade.Modalidade
                    },
                    NumeroAcordo = parcelaUniq.NumeroAcordo.ToString(),
                    Percentual = 0,
                    Politica = true,
                    SaldoDevedor = group.Sum(x => float.Parse(x.SaldoDevedorTotal.ToString())),
                    TotalParcelas = group.Count(),
                    ValorJuros = group.Sum(x => float.Parse(x.JurosParcela.ToString())),
                    ValorMulta = group.Sum(x => float.Parse(x.MultaParcela.ToString())),
                    ValorPago = group.Sum(x => float.Parse(string.IsNullOrEmpty(x.ValorPago.ToString()) ? "0" : x.ValorPago.ToString())),
                    ParcelasAcordadas = new List<BaixaPagamentoParcela>(),
                    ParcelasNegociadas = new List<BaixaPagamentoParcela>()
                };

                foreach (var parcela in group.Where(p => p.TipoRegistro.ToString().Equals("1")))
                {
                    baixaPagamento.ParcelasAcordadas.Add(new BaixaPagamentoParcela()
                    {
                        Agencia = Convert.ToInt32(parcela.CodigoAgencia),
                        AcordoOriginal = Convert.ToInt64(parcela.NumeroAcordo),
                        Banco = Convert.ToInt32(parcela.CodigoBanco),
                        Cheque = Convert.ToInt64(parcela.NumeroCheque),
                        DataBaixa = parcela.DataBaixa.ToString(),
                        DataPagamento = parcela.DataPagamento.ToString(),
                        DataVencimento = parcela.DataVencimentoParcela.ToString(),
                        Parcela = int.Parse(parcela.Parcela.ToString()),
                        Periodo = parcela.Periodo,
                        TipoPagamento = parcela.TipoPagamento,
                        Valor = float.Parse(parcela.ValorParcela.ToString()),
                        ValorPago = float.Parse(string.IsNullOrEmpty(parcela.ValorPago.ToString()) ? "0" : parcela.ValorPago.ToString())
                    });
                }

                foreach (var parcela in group.Where(p => p.TipoRegistro.ToString().Equals("2")))
                {
                    var valorParcelaComJuros = 0.0;
                    var valorParcelaOriginal = Double.Parse(parcela.ValorParcela.ToString());
                    var valorMulta = (valorParcelaOriginal / 100) * 0.2;
                    var valorJurosAoDia = (valorParcelaOriginal / 100) * 0.07;
                    var dataVencimento = parcela.DataVencimentoParcela;
                    var dataAcordo = parcela.DataFechamentoAcordo;
                    var diasVencidos = (dataAcordo - dataVencimento).TotalDays;

                    valorParcelaComJuros = valorParcelaOriginal + valorMulta + (valorJurosAoDia * diasVencidos);

                    var parcelaBaixa = new BaixaPagamentoParcela()
                    {
                        Agencia = Convert.ToInt32(parcela.CodigoAgencia),
                        AcordoOriginal = Convert.ToInt64(parcela.NumeroAcordo),
                        Banco = Convert.ToInt32(parcela.CodigoBanco),
                        Cheque = Convert.ToInt64(parcela.NumeroCheque),
                        DataBaixa = parcela.DataBaixa.ToString(),
                        DataPagamento = parcela.DataPagamento.ToString(),
                        DataVencimento = parcela.DataVencimentoParcela.ToString(),
                        Parcela = int.Parse(parcela.Parcela.ToString()),
                        Periodo = parcela.Periodo,
                        TipoPagamento = parcela.TipoPagamento,
                        Valor = float.Parse(parcela.ValorParcela.ToString()),
                        ValorPago = float.Parse(parcela.ValorPago.ToString()),
                        ValorDebitoOriginal = (decimal)valorParcelaComJuros,
                        NumeroAcordo = Convert.ToDecimal(parcela.NumeroAcordo),
                        Matricula = Convert.ToDecimal(parcela.Matricula),
                        Sistema = parcela.Sistema,
                        IdTitulo = string.IsNullOrEmpty(parcela.IdTitulo.ToString()) ? null : Convert.ToDecimal(parcela.IdTitulo),
                        CodigoAtividade = string.IsNullOrEmpty(parcela.CodigoAtividade.ToString()) ? null : Convert.ToInt32(parcela.CodigoAtividade),
                        NumeroEvt = string.IsNullOrEmpty(parcela.NumeroEvt.ToString()) ? null : Convert.ToInt32(parcela.NumeroEvt),
                        IdPessoa = string.IsNullOrEmpty(parcela.IdPessoa.ToString()) ? null : Convert.ToDecimal(parcela.IdPessoa),
                        NumeroConta = Convert.ToInt32(parcela.NumeroConta),
                        CpfCnpj = parcela.CPF.ToString(),
                        TipoInadimplencia = parcela.TipoInadimplencia
                    };

                    baixaPagamento.ParcelasNegociadas.Add(parcelaBaixa);

                    if (baixaPagamento.Politica)
                        await RegraContemplada(baixaPagamento, parcelaBaixa);
                }
                baixaPagamento.Percentual = (float)(baixaPagamento.ParcelasNegociadas.Sum(p => 100 - (100 * ((decimal)p.ValorPago / p.ValorDebitoOriginal))) / baixaPagamento.ParcelasAcordadas.Count);
                resultadoBaixas.Add(baixaPagamento);
            }

            var modelPaginada = new ModelPaginada<BaixaPagamento>();

            modelPaginada.TotalItems = resultadoBaixas.Count();


            var pagina = (queryParam.Pagina < 1) ? 1 : queryParam.Pagina;
            var limite = (queryParam.Limite < 1) ? 10 : queryParam.Limite;
            var paginaInicial = (pagina - 1) * limite;

            modelPaginada.PaginaAtual = pagina;
            modelPaginada.TamanhoPagina = limite;
            modelPaginada.TotalPaginas = (int)Math.Ceiling(modelPaginada.TotalItems / (double)limite);
            modelPaginada.Items = resultadoBaixas
                                    .Skip(paginaInicial)
                                    .Take(limite)
                                    .ToList();

            return modelPaginada;
        }

        private async Task RegraContemplada(BaixaPagamento baixaPagamento, BaixaPagamentoParcela parcela)
        {
            var _regrasAtivas = (await _regraNegociacaoService.Buscar(new ConsultaRegraNegociacaoQueryParam()
            {
                InstituicaoId = baixaPagamento.InstituicaoModel.Id,
                ModalidadeId = baixaPagamento.ModalidadeModel.Id,
                Pagina = 1,
                Limite = int.MaxValue,
                OrdenarPor = "Id",
                SentidoOrdenacao = "ASC"
            }));
            var regrasAtivas = _regrasAtivas.Items
                /*.Where(r => DateTime.Parse(parcela.DataVencimento) >= r.InadimplenciaInicial.Date
                    && DateTime.Parse(parcela.DataVencimento) <= r.InadimplenciaFinal.Date)
                .Where(r => DateTime.Parse(baixaPagamento.DataNegociacao) >= r.ValidadeInicial.Date
                    && DateTime.Parse(baixaPagamento.DataNegociacao) <= r.ValidadeFinal.Date)*/
                .First();

            var percentual = 100 - (100 * ((decimal)parcela.ValorPago / parcela.ValorDebitoOriginal));

            if (baixaPagamento.FormaPagamento == "AVISTA")
            {
                baixaPagamento.Politica = ((decimal)percentual) <= regrasAtivas.PercentValorAVista;
            }
            else if (baixaPagamento.FormaPagamento == "CARTAO")
            {
                baixaPagamento.Politica = ((decimal)percentual) <= regrasAtivas.PercentValorCartao;
            }
            else if (baixaPagamento.FormaPagamento == "BOLETO")
            {
                baixaPagamento.Politica = ((decimal)percentual) <= regrasAtivas.PercentValorBoleto;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

    }
}
