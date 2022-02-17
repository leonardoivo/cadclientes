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
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class RegraNegociacaoRepository : BaseRepository<RegraNegociacaoModel>, IRegraNegociacaoRepository
    {
        private readonly CacheServiceRepository _cache;
        public RegraNegociacaoRepository(CacheServiceRepository cache, CobrancaAtivaDbContext context) : base(context)
        {
            _cache = cache;
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
            var listCursoModel = _cache.CursoModel;
            var listTituloAvulsoModel = _cache.TituloAvulsoModel;
            var listSituacaoAluno = _cache.SituacaoAlunoModel;
            var listTipoTitulo = _cache.TipoTituloModel;

            var query = DbSet.AsQueryable();

            if (queryParams.InstituicaoId != 0)
                query = query.Where(e => e.Instituicao.Id == queryParams.InstituicaoId);


            if (queryParams.ModalidadeId != 0)
                query = query.Where(e => e.Modalidade.Id == queryParams.ModalidadeId);

            query = TrataFiltroDataValidade(query, queryParams);
            query = TrataFiltroDataInadimplencia(query, queryParams);

            if (queryParams.Status.HasValue)
                query = query.Where(e => e.Status.Equals(queryParams.Status.Value));

            var listRegraNegoquiacao = await query.Include(X => X.Instituicao)
                                                  .Include(X => X.Modalidade)
                                                  .Include(X => X.RegraNegociacaoCurso)
                                                  .Include(X => X.RegraNegociacaoTituloAvulso)
                                                  .Include(X => X.RegraNegociacaoSituacaoAluno)
                                                  .Include(X => X.RegraNegociacaoTipoTitulo).AsSplitQuery().AsNoTracking().ToListAsync();

            // FILTROS
            if (queryParams.Cursos.Length > 0)
            {
                listRegraNegoquiacao = listRegraNegoquiacao.Where(L => L.RegraNegociacaoCurso.Any(R => queryParams.Cursos.Contains(R.CursoId))).ToList();
            }

            if (queryParams.TitulosAvulsos.Length > 0)
            {
                listRegraNegoquiacao = listRegraNegoquiacao.Where(L => L.RegraNegociacaoTituloAvulso.Any(R => queryParams.TitulosAvulsos.Contains(R.TituloAvulsoId))).ToList();
            }

            if (queryParams.SituacoesAlunos.Length > 0)
            {
                listRegraNegoquiacao = listRegraNegoquiacao.Where(L => L.RegraNegociacaoSituacaoAluno.Any(R => queryParams.SituacoesAlunos.Contains(R.SituacaoAlunoId))).ToList();
            }

            if (queryParams.TiposTitulos.Length > 0)
            {
                listRegraNegoquiacao = listRegraNegoquiacao.Where(L => L.RegraNegociacaoTipoTitulo.Any(R => queryParams.TiposTitulos.Contains(R.TipoTituloId))).ToList();
            }

            var listFull = (from r in listRegraNegoquiacao
                            select new BuscaRegraNegociacao()
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
                                Cursos = (from rc in r.RegraNegociacaoCurso
                                          join c in listCursoModel on rc.CursoId equals c.Id
                                          select new CursoModel()
                                          {
                                              Id = c.Id,
                                              Descricao = c.Descricao,
                                              CodigoMagister = c.CodigoMagister,
                                              InstituicaoId = c.InstituicaoId,
                                              ModalidadeId = c.ModalidadeId

                                          }).ToList(),

                                TitulosAvulsos = (from rt in r.RegraNegociacaoTituloAvulso
                                                  join t in listTituloAvulsoModel on rt.TituloAvulsoId equals t.Id
                                                  select new TituloAvulsoModel()
                                                  {
                                                      Id = t.Id,
                                                      Descricao = t.Descricao,
                                                      CodigoGT = t.CodigoGT

                                                  }).ToList(),

                                SituacoesAlunos = (from ra in r.RegraNegociacaoSituacaoAluno
                                                   join a in listSituacaoAluno on ra.SituacaoAlunoId equals a.Id
                                                   select new SituacaoAlunoModel()
                                                   {
                                                       Id = a.Id,
                                                       CodigoMagister = a.CodigoMagister,
                                                       Situacao = a.Situacao
                                                   }).ToList(),

                                TiposTitulos = (from rt in r.RegraNegociacaoTipoTitulo
                                                join t in listTipoTitulo on rt.TipoTituloId equals t.Id
                                                select new TipoTituloModel()
                                                {
                                                    Id = t.Id,
                                                    CodigoMagister = t.CodigoMagister,
                                                    TipoTitulo = t.TipoTitulo

                                                }).ToList(),
                            }).ToList();

            var queryPaginar = listFull.AsQueryable();

            queryPaginar = queryPaginar.Ordenar(queryParams.OrdenarPor, "ValidadeInicial", queryParams.SentidoOrdenacao == "desc");

            return queryPaginar.Paginar(queryParams.Pagina, queryParams.Limite);

            //var modelPaginada = new ModelPaginada<BuscaRegraNegociacao>();

            //modelPaginada.TotalItems = queryPaginar.Count();            
            //modelPaginada.TotalPaginas = (int)Math.Ceiling(modelPaginada.TotalItems / (double)10);
            //modelPaginada.TamanhoPagina = (queryParams.Pagina < 1) ? 1 : queryParams.Pagina;
            //modelPaginada.PaginaAtual = (queryParams.Limite < 1) ? 10 : queryParams.Limite;


            //var skip = modelPaginada.PaginaAtual <= 1 ? 0 : (modelPaginada.PaginaAtual - 1) * modelPaginada.TamanhoPagina;

            //modelPaginada.Items = queryPaginar.Skip(skip)
            //                        .Take(modelPaginada.TamanhoPagina)
            //                        .ToList();


            //return modelPaginada;
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

            query = query.Where(c => c.InstituicaoId == model.InstituicaoId && c.ModalidadeId == model.ModalidadeId);

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

        private static IQueryable<RegraNegociacaoModel> TrataFiltroDataValidade(IQueryable<RegraNegociacaoModel> query,
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

        private static IQueryable<RegraNegociacaoModel> TrataFiltroDataInadimplencia(
            IQueryable<RegraNegociacaoModel> query,
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