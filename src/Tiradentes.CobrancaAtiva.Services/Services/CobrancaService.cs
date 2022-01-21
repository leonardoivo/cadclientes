using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.Validations.RespostaCobranca;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class CobrancaService : BaseService, ICobrancaService
    {
        protected readonly ICobrancaRepository _repositorio;
        protected readonly IAlunosInadimplentesRepository _alunosInadimplentesRepository;
        protected readonly IMapper _map;

        public CobrancaService(ICobrancaRepository repositorio, IAlunosInadimplentesRepository alunosInadimplentesRepository, IMapper map)
        {
            _repositorio = repositorio;
            _alunosInadimplentesRepository = alunosInadimplentesRepository;
            _map = map;
        }

        public async Task<RespostaViewModel> Criar(RespostaViewModel viewModel)
        {
            Validate(new CriarRespostaCobrancaValidation(), viewModel);

            var model = _map.Map<RespostasCollection>(viewModel);

            await _repositorio.Criar(model);

            return _map.Map<RespostaViewModel>(model);
        }

        public async Task<IEnumerable<string>> ListarFiltrosMatricula()
        {
            var baixas = await _repositorio.Listar(new RespostasCollection());

            return baixas.GroupBy(b => b.Matricula).Select(b => b.Key);
        }

        public async Task<IEnumerable<string>> ListarFiltrosAcordo()
        {
            var baixas = await _repositorio.Listar(new RespostasCollection());

            return baixas.GroupBy(b => b.NumeroAcordo).Select(b => b.Key);
        }

        public async Task<IEnumerable<string>> ListarFiltroCpf()
        {
            var baixas = await _repositorio.Listar(new RespostasCollection());

            return baixas.GroupBy(b => b.CPF).Select(b => b.Key);
        }

        public async Task<IEnumerable<string>> ListarFiltroNomeAluno()
        {
            var baixas = await _repositorio.Listar(new RespostasCollection());

            return baixas.GroupBy(b => b.NomeAluno).Select(b => b.Key);
        }

        public async Task<ICollection<BaixaPagamento>> Listar(RespostaViewModel viewModel)
        {
            var resultadoBaixas = new List<BaixaPagamento>();
            var model = _map.Map<RespostasCollection>(viewModel);

            var baixas = await _repositorio.Listar(model);

            var agrupadoPorAcordo = baixas.GroupBy(b => b.NumeroAcordo);

            foreach(var group in agrupadoPorAcordo)
            {
                var parcelaUniq = group.First();

                var baixaPagamento = new BaixaPagamento() {
                        DataBaixa = parcelaUniq.DataBaixa,
                        DataNegociacao = parcelaUniq.DataFechamentoAcordo,
                        EmpresaParceira = parcelaUniq.CnpjEmpresaCobranca,
                        FormaPagamento = parcelaUniq.TipoPagamento,
                        InstituicaoEnsino = "UNIT",
                        Matricula = parcelaUniq.Matricula,
                        ModalidadeEnsino = parcelaUniq.Sistema,
                        NumeroAcordo = parcelaUniq.NumeroAcordo,
                        Percentual = 25.5f,
                        Politica = true,
                        SaldoDevedor = group.Sum(x => float.Parse(x.SaldoDevedorTotal)),
                        TotalParcelas = group.Count(),
                        ValorJuros = group.Sum(x => float.Parse(x.Juros)),
                        ValorMulta = group.Sum(x => float.Parse(x.Multa)),
                        ValorPago = group.Sum(x => float.Parse(string.IsNullOrEmpty(x.ValorPago) ? "0" : x.ValorPago)),
                        ParcelasAcordadas = new List<BaixaPagamentoParcela>(),
                        ParcelasNegociadas = new List<BaixaPagamentoParcela>()
                    };

                foreach(var parcela in group.Where(p => p.TipoRegistro.Equals("1"))) {
                    baixaPagamento.ParcelasAcordadas.Add(new BaixaPagamentoParcela() {
                        Agencia = parcela.CodigoAgencia,
                        AcordoOriginal = parcela.NumeroAcordo,
                        Banco = parcela.CodigoBanco,
                        Cheque = parcela.NumeroCheque,
                        DataBaixa = parcela.DataBaixa,
                        DataPagamento = parcela.DataPagamento,
                        DataVencimento = parcela.DataVencimento,
                        Parcela = int.Parse(parcela.Parcela),
                        Periodo = parcela.Periodo,
                        TipoPagamento = parcela.TipoPagamento,
                        Valor = float.Parse(parcela.ValorParcela),
                        ValorPago = float.Parse(string.IsNullOrEmpty(parcela.ValorPago) ? "0" : parcela.ValorPago)
                    });
                }

                foreach(var parcela in group.Where(p => p.TipoRegistro.Equals("2"))) {
                    baixaPagamento.ParcelasNegociadas.Add(new BaixaPagamentoParcela() {
                        Agencia = parcela.CodigoAgencia,
                        AcordoOriginal = parcela.NumeroAcordo,
                        Banco = parcela.CodigoBanco,
                        Cheque = parcela.NumeroCheque,
                        DataBaixa = parcela.DataBaixa,
                        DataPagamento = parcela.DataPagamento,
                        DataVencimento = parcela.DataVencimento,
                        Parcela = int.Parse(parcela.Parcela),
                        Periodo = parcela.Periodo,
                        TipoPagamento = parcela.TipoPagamento,
                        Valor = float.Parse(parcela.ValorParcela),
                        ValorPago = float.Parse(parcela.ValorPago)
                    });
                }

                resultadoBaixas.Add(baixaPagamento);
            }

            return resultadoBaixas;
        }

        public void Dispose() { }

    }
}
