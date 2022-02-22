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
    public class ParametroEnvioRepository : BaseRepository<ParametroEnvioModel>, IParametroEnvioRepository
    {
        readonly CacheServiceRepository _cache;
        public ParametroEnvioRepository(CacheServiceRepository cache, CobrancaAtivaDbContext context) : base(context)
        {
            _cache = cache;
        }
        public override Task Criar(ParametroEnvioModel model)
        {
            var query = DbSet
                            .Select(r  => new BuscaParametroEnvio {
                                Id = r.Id,
                                EmpresaParceira = r.EmpresaParceira,
                                Instituicao = r.Instituicao,
                                Modalidade = r.Modalidade,
                                Status = r.Status,
                                DiaEnvio = r.DiaEnvio,
                                InadimplenciaInicial = r.InadimplenciaInicial,
                                InadimplenciaFinal = r.InadimplenciaFinal,
                                ValidadeInicial = r.ValidadeInicial,
                                ValidadeFinal = r.ValidadeFinal,
                                Cursos = r.ParametroEnvioCurso.Select(x => x.Curso),
                                TitulosAvulsos = r.ParametroEnvioTituloAvulso.Select(x => x.TituloAvulso),
                                SituacoesAlunos = r.ParametroEnvioSituacaoAluno.Select(x => x.SituacaoAluno),
                                TiposTitulos = r.ParametroEnvioTipoTitulo.Select(x => x.TipoTitulo)
                            })
                            .AsQueryable();

            query = query.Where(e => e.InadimplenciaInicial <= model.InadimplenciaFinal && model.InadimplenciaInicial <= e.InadimplenciaFinal); 

            var regrasCadastradas = query.ToList();

            if(regrasCadastradas.Count > 0)
            {
                /*regrasCadastradas = regrasCadastradas.AsQueryable()
                    .Where(e => e.Instituicao.Id == model.InstituicaoId)
                    .Where(e => e.Cursos.Where(c => model.RegraNegociacaoCurso.Select(c => c.CursoId).Contains(c.Id)).Any())
                    .Where(e => e.Semestres.Where(c => model.RegraNegociacaoSemestre.Select(c => c.SemestreId).Contains(c.Id)).Any())
                    .ToList();*/

                /*if(regrasCadastradas.Count > 0)
                    throw new System.Exception("Parametro envio já cadastrado!");*/
            }

            return base.Criar(model);
        }

        public async Task<List<ParametroEnvioModel>> BuscarApenasParametroEnvio()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<ModelPaginada<BuscaParametroEnvio>> Buscar(ParametroEnvioQueryParam queryParams)
        {
            var listCursoModel = _cache.CursoModel;
            var listTituloAvulsoModel = _cache.TituloAvulsoModel;
            var listSituacaoAluno = _cache.SituacaoAlunoModel;
            var listTipoTitulo = _cache.TipoTituloModel;

            //var query = DbSet
            //                .Select(r => new BuscaParametroEnvio
            //                {
            //                    Id = r.Id,
            //                    EmpresaParceira = r.EmpresaParceira,
            //                    Instituicao = r.Instituicao,
            //                    Modalidade = r.Modalidade,
            //                    Status = r.Status,
            //                    DiaEnvio = r.DiaEnvio,
            //                    MotivoInativacao = r.MotivoInativacao,
            //                    InadimplenciaInicial = r.InadimplenciaInicial,
            //                    InadimplenciaFinal = r.InadimplenciaFinal,
            //                    ValidadeInicial = r.ValidadeInicial,
            //                    ValidadeFinal = r.ValidadeFinal,
            //                    Cursos = r.ParametroEnvioCurso.Select(x => x.Curso),
            //                    TitulosAvulsos = r.ParametroEnvioTituloAvulso.Select(x => x.TituloAvulso),
            //                    SituacoesAlunos = r.ParametroEnvioSituacaoAluno.Select(x => x.SituacaoAluno),
            //                    TiposTitulos = r.ParametroEnvioTipoTitulo.Select(x => x.TipoTitulo)
            //                })
            //                .AsQueryable();

            var query = DbSet.AsQueryable();

            if (queryParams.EmpresaParceiraId != 0)
                query = query.Where(e => e.EmpresaParceira.Id == queryParams.EmpresaParceiraId);

            if (queryParams.InstituicaoId != 0)
                query = query.Where(e => e.Instituicao.Id == queryParams.InstituicaoId);

            if (queryParams.DiaEnvio.HasValue && queryParams.DiaEnvio != 0)
                query = query.Where(e => e.DiaEnvio == queryParams.DiaEnvio);  

            if (queryParams.ValidadeInicial.HasValue)
                query = query.Where(e => e.ValidadeInicial.Day == queryParams.ValidadeInicial.Value.Day && 
                    e.ValidadeInicial.Month == queryParams.ValidadeInicial.Value.Month &&
                    e.ValidadeInicial.Year == queryParams.ValidadeInicial.Value.Year);

            if (queryParams.ValidadeFinal.HasValue)
                query = query.Where(e => e.ValidadeFinal.Day == queryParams.ValidadeFinal.Value.Day && 
                    e.ValidadeFinal.Month == queryParams.ValidadeFinal.Value.Month &&
                    e.ValidadeFinal.Year == queryParams.ValidadeFinal.Value.Year);

            if (queryParams.InadimplenciaInicial.HasValue)
                query = query.Where(e => e.InadimplenciaInicial.Day == queryParams.InadimplenciaInicial.Value.Day
                    && e.InadimplenciaInicial.Month == queryParams.InadimplenciaInicial.Value.Month 
                    && e.InadimplenciaInicial.Year == queryParams.InadimplenciaInicial.Value.Year);

            if (queryParams.InadimplenciaFinal.HasValue)
                query = query.Where(e => e.InadimplenciaFinal.Day == queryParams.InadimplenciaFinal.Value.Day
                    && e.InadimplenciaFinal.Month == queryParams.InadimplenciaFinal.Value.Month 
                    && e.InadimplenciaFinal.Year == queryParams.InadimplenciaFinal.Value.Year);   

            if (queryParams.Status.HasValue)
                query = query.Where(e => e.Status.Equals(queryParams.Status.Value));


            var listParametrosEnvio = await query.Include(X => X.EmpresaParceira)
                                                 .Include(X => X.Instituicao)
                                                 .Include(X => X.Modalidade)
                                                 .Include(X => X.ParametroEnvioCurso)
                                                 .Include(X => X.ParametroEnvioSituacaoAluno)
                                                 .Include(X => X.ParametroEnvioTituloAvulso)
                                                 .Include(X => X.ParametroEnvioTipoTitulo).AsSplitQuery().AsNoTracking().ToListAsync();

            if (queryParams.Modalidades.Length > 0)
            {
                listParametrosEnvio = listParametrosEnvio.Where(P => queryParams.Cursos.Contains(P.Modalidade.Id)).ToList();                
            }

            if (queryParams.Cursos.Length > 0)
            {
                listParametrosEnvio = listParametrosEnvio.Where(P => P.ParametroEnvioCurso.Any(R => queryParams.Cursos.Contains(R.CursoId))).ToList();
            }                

            if (queryParams.TitulosAvulsos.Length > 0)
            {
                listParametrosEnvio = listParametrosEnvio.Where(P => P.ParametroEnvioTituloAvulso.Any(R => queryParams.TitulosAvulsos.Contains(R.Id))).ToList();
            }                

            if (queryParams.SituacoesAlunos.Length > 0)
            {
                listParametrosEnvio = listParametrosEnvio.Where(P => P.ParametroEnvioSituacaoAluno.Any(R => queryParams.SituacoesAlunos.Contains(R.Id))).ToList();
            }                

            if (queryParams.TiposTitulos.Length > 0)
            {
                listParametrosEnvio = listParametrosEnvio.Where(P => P.ParametroEnvioTipoTitulo.Any(R => queryParams.TiposTitulos.Contains(R.Id))).ToList();
            }


            var listFull = (from r in listParametrosEnvio
                            select new BuscaParametroEnvio
                            {
                                Id = r.Id,
                                EmpresaParceira = r.EmpresaParceira,
                                Instituicao = r.Instituicao,
                                Modalidade = r.Modalidade,
                                Status = r.Status,
                                DiaEnvio = r.DiaEnvio,
                                MotivoInativacao = r.MotivoInativacao,
                                InadimplenciaInicial = r.InadimplenciaInicial,
                                InadimplenciaFinal = r.InadimplenciaFinal,
                                ValidadeInicial = r.ValidadeInicial,
                                ValidadeFinal = r.ValidadeFinal,

                                Cursos = (from rc in r.ParametroEnvioCurso
                                          join c in listCursoModel on rc.CursoId equals c.Id
                                          select new CursoModel()
                                          {
                                              Id = c.Id,
                                              Descricao = c.Descricao,
                                              CodigoMagister = c.CodigoMagister,
                                              InstituicaoId = c.InstituicaoId,
                                              ModalidadeId = c.ModalidadeId

                                          }).ToList(),

                                TitulosAvulsos = (from rt in r.ParametroEnvioTituloAvulso
                                                  join t in listTituloAvulsoModel on rt.TituloAvulsoId equals t.Id
                                                  select new TituloAvulsoModel()
                                                  {
                                                      Id = t.Id,
                                                      Descricao = t.Descricao,
                                                      CodigoGT = t.CodigoGT

                                                  }).ToList(),

                                SituacoesAlunos = (from ra in r.ParametroEnvioSituacaoAluno
                                                   join a in listSituacaoAluno on ra.SituacaoAlunoId equals a.Id
                                                   select new SituacaoAlunoModel()
                                                   {
                                                       Id = a.Id,
                                                       CodigoMagister = a.CodigoMagister,
                                                       Situacao = a.Situacao
                                                   }).ToList(),

                                TiposTitulos = (from rt in r.ParametroEnvioTipoTitulo
                                                join t in listTipoTitulo on rt.TipoTituloId equals t.Id
                                                select new TipoTituloModel()
                                                {
                                                    Id = t.Id,
                                                    CodigoMagister = t.CodigoMagister,
                                                    TipoTitulo = t.TipoTitulo

                                                }).ToList(),

                            }).ToList();

            var queryPaginar = listFull.AsQueryable();

            queryPaginar = queryPaginar.Ordenar(queryParams.OrdenarPor, "Id", queryParams.SentidoOrdenacao == "desc");

            return queryPaginar.Paginar(queryParams.Pagina, queryParams.Limite);
        }

        public Task<BuscaParametroEnvio> BuscarPorIdComRelacionamentos(int id)
        {
            return DbSet
                    .Where(r => r.Id.Equals(id))
                    .Select(r => new BuscaParametroEnvio
                    {
                        Id = r.Id,
                        EmpresaParceira = r.EmpresaParceira,
                        Instituicao = r.Instituicao,
                        Modalidade = r.Modalidade,
                        Status = r.Status,
                        DiaEnvio = r.DiaEnvio,
                        MotivoInativacao = r.MotivoInativacao,
                        InadimplenciaInicial = r.InadimplenciaInicial,
                        InadimplenciaFinal = r.InadimplenciaFinal,
                        ValidadeInicial = r.ValidadeInicial,
                        ValidadeFinal = r.ValidadeFinal,
                        Cursos = r.ParametroEnvioCurso.Select(x => x.Curso),
                        TitulosAvulsos = r.ParametroEnvioTituloAvulso.Select(x => x.TituloAvulso),
                        SituacoesAlunos = r.ParametroEnvioSituacaoAluno.Select(x => x.SituacaoAluno),
                        TiposTitulos = r.ParametroEnvioTipoTitulo.Select(x => x.TipoTitulo)
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }

        public override Task<ParametroEnvioModel> BuscarPorId(int id)
        {
            return Db.ParametroEnvio.Include(r => r.ParametroEnvioCurso)
                         .Include(r => r.ParametroEnvioSituacaoAluno)
                         .Include(r => r.ParametroEnvioTipoTitulo)
                         .Include(r => r.ParametroEnvioTituloAvulso)
                         .Include(r => r.EmpresaParceira)
                         .Include(r => r.Instituicao)
                         .Include(r => r.Modalidade)
                         .Where(r => r.Id.Equals(id))
                         .AsNoTracking()
                         .FirstOrDefaultAsync();
        }

        public override Task Alterar(ParametroEnvioModel model)
        {
            Db.ParametroEnvioCurso.RemoveRange(
                Db.ParametroEnvioCurso.Where(
                    c => c.ParametroEnvioId == model.Id && !model.ParametroEnvioCurso.Select(x => x.Id).Contains(c.Id)));
            Db.ParametroEnvioTipoTitulo.RemoveRange(
                Db.ParametroEnvioTipoTitulo.Where(
                    c => c.ParametroEnvioId == model.Id && !model.ParametroEnvioTipoTitulo.Select(x => x.Id).Contains(c.Id)));
            Db.ParametroEnvioSituacaoAluno.RemoveRange(
                Db.ParametroEnvioSituacaoAluno.Where(
                    c => c.ParametroEnvioId == model.Id && !model.ParametroEnvioSituacaoAluno.Select(x => x.Id).Contains(c.Id)));
            Db.ParametroEnvioTituloAvulso.RemoveRange(
                Db.ParametroEnvioTituloAvulso.Where(
                    c => c.ParametroEnvioId == model.Id && !model.ParametroEnvioTituloAvulso.Select(x => x.Id).Contains(c.Id)));
                    
            return base.Alterar(model);
        }

        public override async Task Deletar(int id)
        {
            var modelNoBanco = await BuscarPorId(id);

            if(modelNoBanco == null)
            {
                return;
            }

            Db.ParametroEnvioCurso.RemoveRange(modelNoBanco.ParametroEnvioCurso);
            Db.ParametroEnvioTipoTitulo.RemoveRange(modelNoBanco.ParametroEnvioTipoTitulo);
            Db.ParametroEnvioSituacaoAluno.RemoveRange(modelNoBanco.ParametroEnvioSituacaoAluno);
            Db.ParametroEnvioTituloAvulso.RemoveRange(modelNoBanco.ParametroEnvioTituloAvulso);

            Db.Entry(modelNoBanco).State = EntityState.Deleted;
            await Db.SaveChangesAsync();
        }
    }
}
