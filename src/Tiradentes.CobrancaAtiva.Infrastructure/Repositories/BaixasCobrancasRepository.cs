using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories.Helpers;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class BaixasCobrancasRepository : BaseRepository<BaixasCobrancasModel>, IBaixasCobrancasRepository
    {
        public BaixasCobrancasRepository(CobrancaAtivaDbContext context) : base(context)
        {
        }

        public async Task<BaixasCobrancasModel> BuscarPorDataBaixa(DateTime dataBaixa)
        {
            return await DbSet.Where(B => B.DataBaixa.Year == dataBaixa.Year
                                          && B.DataBaixa.Month == dataBaixa.Month
                                          && B.DataBaixa.Day == dataBaixa.Day).FirstOrDefaultAsync();
        }

        public void HabilitarAlteracaoBaixaCobranca(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_baix_cob({status.ToString().ToLower()});
                                         end;");
        }

        public async Task<ModelPaginada<BuscaBaixaPagamentoDto>> Buscar(BaixaCobrancaQueryParam queryParam)
        {
            if (queryParam.EmpresaParceiraId != null)
                queryParam.CnpjEmpresaParceira =
                    await Db.EmpresaParceira.Where(e => queryParam.EmpresaParceiraId.Contains(e.Id))
                        .Select(e => e.CNPJ)
                        .ToListAsync();

            var dados = await Db.ItensBaixaTipo1.FiltrarItensBaixaPagamento(queryParam)
                .Select(i1 => new
                {
                    cnpj = i1.CnpjEmpresaCobranca,
                    dtBaixa = i1.DataBaixa
                })
                .Concat(Db.ItensBaixaTipo2.FiltrarItensBaixaPagamento(queryParam)
                    .Select(i1 => new
                    {
                        cnpj = i1.CnpjEmpresaCobranca,
                        dtBaixa = i1.DataBaixa
                    }))
                .Concat(Db.ItensBaixaTipo3.FiltrarItensBaixaPagamento(queryParam)
                    .Select(i1 => new
                    {
                        cnpj = i1.CnpjEmpresaCobranca,
                        dtBaixa = i1.DataBaixa
                    }))
                .OrderByDescending(i => i.dtBaixa)
                .GroupBy(i => new {i.cnpj, i.dtBaixa})
                .Select(i => new
                {
                    i.Key.cnpj,
                    i.Key.dtBaixa
                })
                .Paginar(0, 100);


            var resultado = new ModelPaginada<BuscaBaixaPagamentoDto>
            {
                Items = new List<BuscaBaixaPagamentoDto>(),
                PaginaAtual = dados.PaginaAtual,
                TamanhoPagina = dados.TamanhoPagina,
                TotalItems = dados.TotalItems,
                TotalPaginas = dados.TotalPaginas
            };
            foreach (var item in dados.Items)
            {
                var newObj = new BuscaBaixaPagamentoDto
                {
                    DataBaixa = item.dtBaixa,
                    NomeEmpresaParceira = (await Db.EmpresaParceira.FirstOrDefaultAsync(e => e.CNPJ == item.cnpj))
                        ?.NomeFantasia,
                    NomeInstituicaoEnsino = (await Db.Instituicao.FirstOrDefaultAsync(i => i.Id == 58))?.Instituicao,
                    Items = await BuscarItems(item.dtBaixa, item.cnpj, queryParam)
                };
                resultado.Items.Add(newObj);
            }

            return resultado;
        }

        private async Task<IEnumerable<ItensBaixaDto>> BuscarItems(DateTime dtBaixa, string cnpj,
            BaixaCobrancaQueryParam queryParam)
        {
            var itemTipo1 = await Db.ItensBaixaTipo1.BuscarItems(dtBaixa, cnpj, queryParam)
                .Select(i => new ItensBaixaDto
                {
                    Tipo = 1,
                    Erro = i.CodigoErro,
                    Juros = i.Juros,
                    Matricula = i.Matricula,
                    Multa = i.Multa,
                    Parcela = i.Parcela,
                    Valor = i.Valor,
                    DataVencimento = i.DataVencimento,
                    NumeroAcordo = i.NumeroAcordo,
                    NumeroLinha = i.NumeroLinha,
                    NomeAluno = "Teste"
                }).ToListAsync();

            var itemTipo2 = await Db.ItensBaixaTipo2.BuscarItems(dtBaixa, cnpj, queryParam)
                .Select(i => new ItensBaixaDto
                {
                    Tipo = 2,
                    Erro = i.CodigoErro,
                    Matricula = i.Matricula,
                    Parcela = i.Parcela,
                    Valor = i.Valor,
                    DataVencimento = i.DataVencimento,
                    NumeroAcordo = i.NumeroAcordo,
                    NumeroLinha = i.NumeroLinha,
                    NomeAluno = "Teste"
                }).ToListAsync();

            var itemTipo3 = await Db.ItensBaixaTipo3.BuscarItems(dtBaixa, cnpj, queryParam)
                .Select(i => new ItensBaixaDto
                {
                    Tipo = 3,
                    Erro = i.CodigoErro,
                    Matricula = i.Matricula,
                    Parcela = i.Parcela,
                    Valor = i.ValorPago,
                    NumeroAcordo = i.NumeroAcordo,
                    NumeroLinha = i.NumeroLinha,
                    NomeAluno = "Teste",
                }).ToListAsync();

            return itemTipo1.Concat(itemTipo2).Concat(itemTipo3).OrderBy(i => i.NumeroLinha).ThenBy(i => i.NomeAluno);
        }
    }
}