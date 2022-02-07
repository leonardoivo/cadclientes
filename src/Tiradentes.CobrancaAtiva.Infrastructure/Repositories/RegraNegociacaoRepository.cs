using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories.Helpers;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class RegraNegociacaoRepository : BaseRepository<RegraNegociacaoModel>, IRegraNegociacaoRepository
    {
        public RegraNegociacaoRepository(CobrancaAtivaDbContext context) : base(context)
        {
        }

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
            query = query.Where(e =>
                e.ValidadeInicial <= System.DateTime.Now && e.ValidadeFinal >= System.DateTime.Now);

            var regrasCadastradas = await query.ToListAsync();

            return regrasCadastradas;
        }

        public override Task Criar(RegraNegociacaoModel model)
        {
            var query = DbSet
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
                .AsQueryable();

            query = query.Where(e => e.Status == true).Where(e => e.Modalidade.Id == model.ModalidadeId);

            query = query.Where(e =>
                e.InadimplenciaInicial <= model.InadimplenciaFinal &&
                model.InadimplenciaInicial <= e.InadimplenciaFinal);

            var regrasCadastradas = query.ToList();

            if (regrasCadastradas.Count > 0)
            {
                regrasCadastradas = regrasCadastradas.AsQueryable().Where(e =>
                        (e.Instituicao == null ? e.Instituicao == null : (e.Instituicao.Id == model.InstituicaoId)))
                    .ToList();

                if (model.RegraNegociacaoCurso.Count > 0)
                    regrasCadastradas = regrasCadastradas.Where(e =>
                            e.Cursos.Where(c => model.RegraNegociacaoCurso.Select(c => c.CursoId).Contains(c.Id)).Any())
                        .ToList();

                if (model.RegraNegociacaoTipoTitulo.Count > 0)
                    regrasCadastradas = regrasCadastradas.Where(e =>
                        e.TiposTitulos.Where(c =>
                            model.RegraNegociacaoTipoTitulo.Select(c => c.TipoTituloId).Contains(c.Id)).Any()).ToList();

                if (regrasCadastradas.Count > 0)
                    throw new System.Exception("Regra já cadastrada!");
            }

            return base.Criar(model);
        }

        public async Task<ModelPaginada<BuscaRegraNegociacao>> Buscar(RegraNegociacaoQueryParam queryParams)
        {
            var query = DbSet
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
                .AsQueryable();

            if (queryParams.InstituicaoId != 0)
                query = query.Where(e => e.Instituicao.Id == queryParams.InstituicaoId);

            if (queryParams.ModalidadeId != 0)
                query = query.Where(e => e.Modalidade.Id == queryParams.ModalidadeId);

            query = TrataFiltroDataValidade(query, queryParams);
            query = TrataFiltroDataInadimplencia(query, queryParams);

            if (queryParams.Cursos.Length > 0)
                query = query.Where(e => e.Cursos.Where(c => queryParams.Cursos.Contains(c.Id)).Any());

            if (queryParams.TitulosAvulsos.Length > 0)
                query = query.Where(e => e.TitulosAvulsos.Where(c => queryParams.TitulosAvulsos.Contains(c.Id)).Any());

            if (queryParams.SituacoesAlunos.Length > 0)
                query = query.Where(e =>
                    e.SituacoesAlunos.Where(c => queryParams.SituacoesAlunos.Contains(c.Id)).Any());

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
                    c => c.RegraNegociacaoId == model.Id &&
                         !model.RegraNegociacaoCurso.Select(x => x.Id).Contains(c.Id)));
            Db.RegraNegociacaoTituloAvulso.RemoveRange(
                Db.RegraNegociacaoTituloAvulso.Where(
                    c => c.RegraNegociacaoId == model.Id &&
                         !model.RegraNegociacaoTituloAvulso.Select(x => x.Id).Contains(c.Id)));
            Db.RegraNegociacaoSituacaoAluno.RemoveRange(
                Db.RegraNegociacaoSituacaoAluno.Where(
                    c => c.RegraNegociacaoId == model.Id &&
                         !model.RegraNegociacaoSituacaoAluno.Select(x => x.Id).Contains(c.Id)));
            Db.RegraNegociacaoTipoTitulo.RemoveRange(
                Db.RegraNegociacaoTipoTitulo.Where(
                    c => c.RegraNegociacaoId == model.Id &&
                         !model.RegraNegociacaoTipoTitulo.Select(x => x.Id).Contains(c.Id)));

            return base.Alterar(model);
        }

        public async Task<RegraNegociacaoModel> VerificarRegraConflitante(RegraNegociacaoModel model)
        {
            var query = DbSet
                .Include(r => r.Instituicao)
                .Include(r => r.Modalidade)
                .AsQueryable();

            query = query.Where(e =>
                (e.ValidadeInicial.Date <= model.ValidadeInicial.Date &&
                 e.ValidadeFinal.Date >= model.ValidadeFinal.Date)
                ||
                (e.ValidadeInicial.Date >= model.ValidadeInicial.Date &&
                 e.ValidadeInicial.Date <= model.ValidadeFinal.Date)
                ||
                (e.ValidadeInicial.Date <= model.ValidadeInicial.Date &&
                 e.ValidadeFinal.Date >= model.ValidadeInicial.Date)
                ||
                (e.ValidadeFinal.Date >= model.ValidadeFinal.Date && e.ValidadeInicial.Date <= model.ValidadeFinal.Date)
            );

            query = query.Where(e => e.PercentJurosMultaAVista != model.PercentJurosMultaAVista
                                     || e.PercentValorAVista != model.PercentValorAVista
                                     || e.PercentJurosMultaCartao != model.PercentJurosMultaCartao
                                     || e.PercentValorCartao != model.PercentValorCartao
                                     || e.QuantidadeParcelasCartao != model.QuantidadeParcelasCartao
                                     || e.PercentJurosMultaBoleto != model.PercentJurosMultaBoleto
                                     || e.PercentValorBoleto != model.PercentValorBoleto
                                     || e.QuantidadeParcelasBoleto != model.QuantidadeParcelasBoleto
                                     || e.PercentEntradaBoleto != model.PercentEntradaBoleto);

            query = query.Where(e => e.Status == true);

            var regraConflitante = await query.FirstOrDefaultAsync();

            return regraConflitante;
        }

        private static IQueryable<BuscaRegraNegociacao> TrataFiltroDataValidade(IQueryable<BuscaRegraNegociacao> query,
            RegraNegociacaoQueryParam queryParams)
        {
            if (!queryParams.ValidadeInicial.HasValue && !queryParams.ValidadeFinal.HasValue) return query;
            if (queryParams.ValidadeInicial.HasValue && queryParams.ValidadeFinal.HasValue)
            {
                var validadeInicial = queryParams.ValidadeInicial.Value;
                var validadeFinal = queryParams.ValidadeFinal.Value;
                return query.Where(r => (r.ValidadeInicial.Date <= validadeInicial.Date
                                         && r.ValidadeFinal.Date >= validadeInicial.Date)
                                        || (r.ValidadeInicial.Date <= validadeFinal.Date
                                            && r.ValidadeFinal.Date >= validadeFinal.Date));
            }

            if (queryParams.ValidadeInicial.HasValue)
            {
                var validadeInicial = queryParams.ValidadeInicial.Value;
                return query.Where(r => r.ValidadeInicial.Date <= validadeInicial.Date
                                        && r.ValidadeFinal.Date >= validadeInicial.Date);
            }

            var validade = queryParams.ValidadeFinal.Value;
            return query.Where(r => r.ValidadeInicial.Date <= validade.Date
                                    && r.ValidadeFinal.Date >= validade.Date);
        }

        private static IQueryable<BuscaRegraNegociacao> TrataFiltroDataInadimplencia(
            IQueryable<BuscaRegraNegociacao> query,
            RegraNegociacaoQueryParam queryParams)
        {
            if (!queryParams.InadimplenciaInicial.HasValue && !queryParams.InadimplenciaFinal.HasValue) return query;
            if (queryParams.InadimplenciaInicial.HasValue && queryParams.InadimplenciaFinal.HasValue)
            {
                var inadimplenciaInicial = RetornaDataInicial(queryParams.InadimplenciaInicial.Value);
                var inadimplenciaFinal = RetornaDataFinal(queryParams.InadimplenciaFinal.Value);
                return query.Where(r => (r.InadimplenciaInicial.Date <= inadimplenciaInicial.Date
                                         && r.InadimplenciaFinal.Date >= inadimplenciaInicial.Date)
                                        || (r.InadimplenciaInicial.Date <= inadimplenciaFinal.Date
                                            && r.InadimplenciaFinal.Date >= inadimplenciaFinal.Date));
            }

            if (queryParams.InadimplenciaInicial.HasValue)
            {
                var inadimplenciaInicial = RetornaDataInicial(queryParams.InadimplenciaInicial.Value);
                return query.Where(r => r.InadimplenciaInicial.Date <= inadimplenciaInicial.Date
                                        && r.InadimplenciaFinal.Date >= inadimplenciaInicial.Date);
            }

            var inadimplencia = RetornaDataFinal(queryParams.InadimplenciaFinal.Value);
            return query.Where(r => r.InadimplenciaInicial.Date <= inadimplencia.Date
                                    && r.InadimplenciaFinal.Date >= inadimplencia.Date);
        }

        private static DateTime RetornaDataInicial(DateTime data)
        {
            return new DateTime(data.Year, data.Month, 1);
        }

        private static DateTime RetornaDataFinal(DateTime data)
        {
            var ultimoDiaMes = DateTime.DaysInMonth(data.Year, data.Month);
            return new DateTime(data.Year, data.Month, ultimoDiaMes);
        }
    }
}