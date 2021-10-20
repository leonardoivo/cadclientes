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

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ParametroEnvioRepository : BaseRepository<ParametroEnvioModel>, IParametroEnvioRepository
    {
        public ParametroEnvioRepository(CobrancaAtivaDbContext context) : base(context)
        { }
        public override Task Criar(ParametroEnvioModel model)
        {
            var query = DbSet
                            .Select(r  => new BuscaParametroEnvio {
                                Id = r.Id,
                                Instituicao = r.Instituicao,
                                Modalidade = r.Modalidade,
                                EmpresaParceira = r.EmpresaParceira,
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

            query = query.Where(e => e.Status == true).Where(e => e.Modalidade.Id == model.ModalidadeId);

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

        public async Task<ModelPaginada<BuscaParametroEnvio>> Buscar(ParametroEnvioQueryParam queryParams)
        {
            var query = DbSet
                            .Select(r  => new BuscaParametroEnvio {
                                Id = r.Id,
                                Instituicao = r.Instituicao,
                                Modalidade = r.Modalidade,
                                EmpresaParceira = r.EmpresaParceira,
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

            if (queryParams.InstituicaoId != 0)
                query = query.Where(e => e.Instituicao.Id == queryParams.InstituicaoId);

            if (queryParams.ModalidadeId != 0)
                query = query.Where(e => e.Modalidade.Id == queryParams.ModalidadeId);  

            if (queryParams.EmpresaParceiraId != 0)
                query = query.Where(e => e.EmpresaParceira.Id == queryParams.EmpresaParceiraId);

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
                query = query.Where(e => e.InadimplenciaInicial.Month == queryParams.InadimplenciaInicial.Value.Month 
                    && e.InadimplenciaInicial.Year == queryParams.InadimplenciaInicial.Value.Year);

            if (queryParams.InadimplenciaFinal.HasValue)
                query = query.Where(e => e.InadimplenciaFinal.Month == queryParams.InadimplenciaFinal.Value.Month 
                    && e.InadimplenciaFinal.Year == queryParams.InadimplenciaFinal.Value.Year);   

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

            return await query.OrderBy(e => e.Id).Paginar(queryParams.Pagina, queryParams.Limite);
        }

        public Task<BuscaParametroEnvio> BuscarPorIdComRelacionamentos(int id)
        {
            return DbSet
                    .Where(r => r.Id.Equals(id))
                    .Select(r => new BuscaParametroEnvio
                    {
                        Id = r.Id,
                        Instituicao = r.Instituicao,
                        Modalidade = r.Modalidade,
                        EmpresaParceira = r.EmpresaParceira,
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
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }

        public override Task<ParametroEnvioModel> BuscarPorId(int id)
        {
            return DbSet.Include(r => r.ParametroEnvioCurso)
                         .Include(r => r.ParametroEnvioSituacaoAluno)
                         .Include(r => r.ParametroEnvioTipoTitulo)
                         .Include(r => r.ParametroEnvioTituloAvulso)
                         .Include(r => r.Modalidade)
                         .Include(r => r.Instituicao)
                         .Include(r => r.EmpresaParceira)
                         .Where(r => r.Id.Equals(id))
                         .AsNoTracking()
                         .FirstOrDefaultAsync();
        }

        public override Task Alterar(ParametroEnvioModel model)
        {
            Db.ParametroEnvioCurso.RemoveRange(
                Db.ParametroEnvioCurso.Where(
                    c => !model.ParametroEnvioCurso.Select(x => x.Id).Contains(c.Id)));
            Db.ParametroEnvioTipoTitulo.RemoveRange(
                Db.ParametroEnvioTipoTitulo.Where(
                    c => !model.ParametroEnvioTipoTitulo.Select(x => x.Id).Contains(c.Id)));
            Db.ParametroEnvioSituacaoAluno.RemoveRange(
                Db.ParametroEnvioSituacaoAluno.Where(
                    c => !model.ParametroEnvioSituacaoAluno.Select(x => x.Id).Contains(c.Id)));
            Db.ParametroEnvioTituloAvulso.RemoveRange(
                Db.ParametroEnvioTituloAvulso.Where(
                    c => !model.ParametroEnvioTituloAvulso.Select(x => x.Id).Contains(c.Id)));
                    
            return base.Alterar(model);
        }
    }
}
