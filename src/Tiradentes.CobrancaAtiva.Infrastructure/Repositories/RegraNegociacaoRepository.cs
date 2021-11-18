﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class RegraNegociacaoRepository : BaseRepository<RegraNegociacaoModel>, IRegraNegociacaoRepository
    {
        public RegraNegociacaoRepository(CobrancaAtivaDbContext context) : base(context)
        { }

        public async Task<List<RegraNegociacaoModel>> ListarRegrasParaInativar()
        {
            var query = DbSet
                            .Include(r => r.Instituicao)
                            .Include(r => r.Modalidade)
                            .Include(r => r.RegraNegociacaoCurso)
                            .Include(r => r.RegraNegociacaoTituloAvulso)
                            .Include(r => r.RegraNegociacaoSituacaoAluno)
                            .Include(r => r.RegraNegociacaoTipoTitulo)
                            .AsQueryable();

            query = query.Where(e => e.Status == true);
            query = query.Where(e => e.ValidadeFinal <= System.DateTime.Now);

            var regrasCadastradas = await query.ToListAsync();

            return regrasCadastradas;
        }

        public async Task<List<RegraNegociacaoModel>> ListarRegrasParaAtivar()
        {
            var query = DbSet
                            .Include(r => r.Instituicao)
                            .Include(r => r.Modalidade)
                            .Include(r => r.RegraNegociacaoCurso)
                            .Include(r => r.RegraNegociacaoTituloAvulso)
                            .Include(r => r.RegraNegociacaoSituacaoAluno)
                            .Include(r => r.RegraNegociacaoTipoTitulo)
                            .AsQueryable();

            query = query.Where(e => e.Status == false);
            query = query.Where(e => e.ValidadeInicial <= System.DateTime.Now && e.ValidadeFinal >= System.DateTime.Now);

            var regrasCadastradas = await query.ToListAsync();

            return regrasCadastradas;
        }

        public override Task Criar(RegraNegociacaoModel model)
        {
            var query = DbSet
                            .Select(r  => new BuscaRegraNegociacao {
                                Id = r.Id,
                                Instituicao = r.Instituicao,
                                Modalidade = r.Modalidade,
                                PercentJurosMultaAVista = r.PercentJurosMultaAVista,
                                PercentValorAVista = r.PercentValorAVista,
                                PercentJurosMultaCartao = r.PercentJurosMultaCartao,
                                PercentValorCartao = r.PercentValorCartao,
                                QuantidadeParcelasCartao = r.QuantidadeParcelasCartao,
                                PercentJurosMultaBoleto = r.PercentJurosMultaBoleto,
                                PercentValorBoleto = r.PercentValorBoleto,
                                QuantidadeParcelasBoleto = r.QuantidadeParcelasBoleto,
                                PercentEntradaBoleto = r.PercentEntradaBoleto,
                                Status = r.Status,
                                InadimplenciaInicial = r.InadimplenciaInicial,
                                InadimplenciaFinal = r.InadimplenciaFinal,
                                ValidadeInicial = r.ValidadeInicial,
                                ValidadeFinal = r.ValidadeFinal,
                                Cursos = r.RegraNegociacaoCurso.Select(x => x.Curso),
                                TitulosAvulsos = r.RegraNegociacaoTituloAvulso.Select(x => x.TituloAvulso),
                                SituacoesAlunos = r.RegraNegociacaoSituacaoAluno.Select(x => x.SituacaoAluno),
                                TiposTitulos = r.RegraNegociacaoTipoTitulo.Select(x => x.TipoTitulo)
                            })
                            .AsQueryable();

            query = query.Where(e => e.Status == true).Where(e => e.Modalidade.Id == model.ModalidadeId);

            query = query.Where(e => e.InadimplenciaInicial <= model.InadimplenciaFinal && model.InadimplenciaInicial <= e.InadimplenciaFinal); 

            var regrasCadastradas = query.ToList();

            if(regrasCadastradas.Count > 0)
            {
                regrasCadastradas = regrasCadastradas.AsQueryable()
                    .Where(e => e.Instituicao.Id == model.InstituicaoId)
                    .Where(e => e.Cursos.Where(c => model.RegraNegociacaoCurso.Select(c => c.CursoId).Contains(c.Id)).Any())
                    .Where(e => e.TiposTitulos.Where(c => model.RegraNegociacaoTipoTitulo.Select(c => c.TipoTituloId).Contains(c.Id)).Any())
                    .ToList();

                if(regrasCadastradas.Count > 0)
                    throw new System.Exception("Regra já cadastrada!");
            }

            return base.Criar(model);
        }

        public async Task<ModelPaginada<BuscaRegraNegociacao>> Buscar(RegraNegociacaoQueryParam queryParams)
        {
            var query = DbSet
                            .Select(r  => new BuscaRegraNegociacao {
                                Id = r.Id,
                                Instituicao = r.Instituicao,
                                Modalidade = r.Modalidade,
                                PercentJurosMultaAVista = r.PercentJurosMultaAVista,
                                PercentValorAVista = r.PercentValorAVista,
                                PercentJurosMultaCartao = r.PercentJurosMultaCartao,
                                PercentValorCartao = r.PercentValorCartao,
                                QuantidadeParcelasCartao = r.QuantidadeParcelasCartao,
                                PercentJurosMultaBoleto = r.PercentJurosMultaBoleto,
                                PercentValorBoleto = r.PercentValorBoleto,
                                QuantidadeParcelasBoleto = r.QuantidadeParcelasBoleto,
                                PercentEntradaBoleto = r.PercentEntradaBoleto,
                                Status = r.Status,
                                InadimplenciaInicial = r.InadimplenciaInicial,
                                InadimplenciaFinal = r.InadimplenciaFinal,
                                ValidadeInicial = r.ValidadeInicial,
                                ValidadeFinal = r.ValidadeFinal,
                                Cursos = r.RegraNegociacaoCurso.Select(x => x.Curso),
                                TitulosAvulsos = r.RegraNegociacaoTituloAvulso.Select(x => x.TituloAvulso),
                                SituacoesAlunos = r.RegraNegociacaoSituacaoAluno.Select(x => x.SituacaoAluno),
                                TiposTitulos = r.RegraNegociacaoTipoTitulo.Select(x => x.TipoTitulo)
                            })
                            .AsQueryable();

            if (queryParams.InstituicaoId != 0)
                query = query.Where(e => e.Instituicao.Id == queryParams.InstituicaoId);

            if (queryParams.ModalidadeId != 0)
                query = query.Where(e => e.Modalidade.Id == queryParams.ModalidadeId);   

            if (queryParams.ValidadeInicial.HasValue)
                query = query.Where(e => e.ValidadeInicial.Day == queryParams.ValidadeInicial.Value.Day && 
                    e.ValidadeInicial.Month == queryParams.ValidadeInicial.Value.Month &&
                    e.ValidadeInicial.Year == queryParams.ValidadeInicial.Value.Year);

            if (queryParams.ValidadeFinal.HasValue)
                query = query.Where(e => e.ValidadeFinal.Day == queryParams.ValidadeFinal.Value.Day && 
                    e.ValidadeFinal.Month == queryParams.ValidadeFinal.Value.Month &&
                    e.ValidadeFinal.Year == queryParams.ValidadeFinal.Value.Year);

            if (queryParams.InadimplenciaInicial.HasValue)
                query = query.Where(e => e.InadimplenciaInicial.Month == queryParams.InadimplenciaInicial.Value.Month && e.InadimplenciaInicial.Year == queryParams.InadimplenciaInicial.Value.Year);

            if (queryParams.InadimplenciaFinal.HasValue)
                query = query.Where(e => e.InadimplenciaFinal.Month == queryParams.InadimplenciaFinal.Value.Month && e.InadimplenciaFinal.Year == queryParams.InadimplenciaFinal.Value.Year);   

            if (queryParams.Cursos.Length > 0)
                query = query.Where(e => e.Cursos.Where(c => queryParams.Cursos.Contains(c.Id)).Any());

            if (queryParams.TitulosAvulsos.Length > 0)
                 query = query.Where(e => e.TitulosAvulsos.Where(c => queryParams.TitulosAvulsos.Contains(c.Id)).Any());

            if (queryParams.SituacoesAlunos.Length > 0)
                query = query.Where(e => e.SituacoesAlunos.Where(c => queryParams.SituacoesAlunos.Contains(c.Id)).Any());

            if (queryParams.TiposTitulos.Length > 0)
                query = query.Where(e => e.TiposTitulos.Where(c => queryParams.TiposTitulos.Contains(c.Id)).Any());

            if (queryParams.Status.HasValue)
                query = query.Where(e => e.Status.Equals(queryParams.Status.Value));

            query = query.Ordenar(queryParams.OrdenarPor, "ValidadeInicial", queryParams.SentidoOrdenacao == "desc");

            return await query.Paginar(queryParams.Pagina, queryParams.Limite);
        }

        public Task<BuscaRegraNegociacao> BuscarPorIdComRelacionamentos(int id)
        {
            return DbSet
                    .Where(r => r.Id.Equals(id))
                    .Select(r => new BuscaRegraNegociacao
                    {
                        Id = r.Id,
                        Instituicao = r.Instituicao,
                        Modalidade = r.Modalidade,
                        PercentJurosMultaAVista = r.PercentJurosMultaAVista,
                        PercentValorAVista = r.PercentValorAVista,
                        PercentJurosMultaCartao = r.PercentJurosMultaCartao,
                        PercentValorCartao = r.PercentValorCartao,
                        QuantidadeParcelasCartao = r.QuantidadeParcelasCartao,
                        PercentJurosMultaBoleto = r.PercentJurosMultaBoleto,
                        PercentValorBoleto = r.PercentValorBoleto,
                        QuantidadeParcelasBoleto = r.QuantidadeParcelasBoleto,
                        PercentEntradaBoleto = r.PercentEntradaBoleto,
                        Status = r.Status,
                        InadimplenciaInicial = r.InadimplenciaInicial,
                        InadimplenciaFinal = r.InadimplenciaFinal,
                        ValidadeInicial = r.ValidadeInicial,
                        ValidadeFinal = r.ValidadeFinal,
                        Cursos = r.RegraNegociacaoCurso.Select(x => x.Curso),
                        TitulosAvulsos = r.RegraNegociacaoTituloAvulso.Select(x => x.TituloAvulso),
                        SituacoesAlunos = r.RegraNegociacaoSituacaoAluno.Select(x => x.SituacaoAluno),
                        TiposTitulos = r.RegraNegociacaoTipoTitulo.Select(x => x.TipoTitulo)
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }

        public override Task<RegraNegociacaoModel> BuscarPorId(int id)
        {
            return DbSet.Include(r => r.RegraNegociacaoCurso)
                         .Include(r => r.RegraNegociacaoTituloAvulso)
                         .Include(r => r.RegraNegociacaoSituacaoAluno)
                         .Include(r => r.RegraNegociacaoTipoTitulo)
                         .Where(r => r.Id.Equals(id))
                         .AsNoTracking()
                         .FirstOrDefaultAsync();
        }

        public override Task Alterar(RegraNegociacaoModel model)
        {
            Db.RegraNegociacaoCurso.RemoveRange(
                Db.RegraNegociacaoCurso.Where(
                    c => !model.RegraNegociacaoCurso.Select(x => x.Id).Contains(c.Id)));
            Db.RegraNegociacaoTituloAvulso.RemoveRange(
                Db.RegraNegociacaoTituloAvulso.Where(
                    c => !model.RegraNegociacaoTituloAvulso.Select(x => x.Id).Contains(c.Id)));
            Db.RegraNegociacaoSituacaoAluno.RemoveRange(
                Db.RegraNegociacaoSituacaoAluno.Where(
                    c => !model.RegraNegociacaoSituacaoAluno.Select(x => x.Id).Contains(c.Id)));
            Db.RegraNegociacaoTipoTitulo.RemoveRange(
                Db.RegraNegociacaoTipoTitulo.Where(
                    c => !model.RegraNegociacaoTipoTitulo.Select(x => x.Id).Contains(c.Id)));
                    
            return base.Alterar(model);
        }
    }
}
