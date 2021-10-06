using System.Collections.Generic;
using System.Linq;
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

        public async Task<ModelPaginada<BuscaRegraNegociacao>> Buscar(RegraNegociacaoQueryParam queryParams)
        {
            var query = DbSet
                            .Select(r  => new BuscaRegraNegociacao {
                                Id = r.Id,
                                Instituicao = r.Instituicao,
                                Modalidade = r.Modalidade,
                                PercentJurosMulta = r.PercentJurosMulta,
                                PercentValor = r.PercentValor,
                                Status = r.Status,
                                MesAnoInicial = r.MesAnoInicial,
                                MesAnoFinal = r.MesAnoFinal,
                                ValidadeInicial = r.ValidadeInicial,
                                ValidadeFinal = r.ValidadeFinal,
                                Cursos = r.RegraNegociacaoCurso.Select(x => x.Curso),
                                Semestres = r.RegraNegociacaoSemestre.Select(x => x.Semestre),
                                SituacoesAlunos = r.RegraNegociacaoSituacaoAluno.Select(x => x.SituacaoAluno),
                                TiposPagamentos = r.RegraNegociacaoTipoPagamento.Select(x => x.TipoPagamento),
                                TiposTitulos = r.RegraNegociacaoTipoTitulo.Select(x => x.TipoTitulo)
                            })
                            .AsQueryable();

            if (queryParams.InstituicaoId != 0)
                query = query.Where(e => e.Instituicao.Id == queryParams.InstituicaoId);

            if (queryParams.ModalidadeId != 0)
                query = query.Where(e => e.Modalidade.Id == queryParams.ModalidadeId);   

            if (queryParams.ValidadeInicial.HasValue)
                query = query.Where(e => e.ValidadeInicial.ToString("dd/MM/yyyy").Equals(queryParams.ValidadeInicial.Value.ToString("dd/MM/yyyy")));

            if (queryParams.ValidadeFinal.HasValue)
                query = query.Where(e => e.ValidadeFinal.ToString("dd/MM/yyyy").Equals(queryParams.ValidadeFinal.Value.ToString("dd/MM/yyyy")));

            if (queryParams.MesAnoInicial.HasValue)
                query = query.Where(e => e.MesAnoInicial.ToString("MM/yyyy").Equals(queryParams.MesAnoInicial.Value.ToString("MM/yyyy")));

            if (queryParams.MesAnoFinal.HasValue)
                query = query.Where(e => e.MesAnoFinal.ToString("MM/yyyy").Equals(queryParams.MesAnoFinal.Value.ToString("MM/yyyy")));   

            if (queryParams.Cursos.Length > 0)
                query = query.Where(e => e.Cursos.Where(c => queryParams.Cursos.Contains(c.Id)).Any());

            if (queryParams.Semestres.Length > 0)
                query = query.Where(e => e.Semestres.Where(c => queryParams.Semestres.Contains(c.Id)).Any());

            if (queryParams.TiposPagamentos.Length > 0)
                query = query.Where(e => e.TiposPagamentos.Where(c => queryParams.TiposPagamentos.Contains(c.Id)).Any());

            if (queryParams.SituacoesAlunos.Length > 0)
                query = query.Where(e => e.SituacoesAlunos.Where(c => queryParams.SituacoesAlunos.Contains(c.Id)).Any());

            if (queryParams.TiposTitulos.Length > 0)
                query = query.Where(e => e.TiposTitulos.Where(c => queryParams.TiposTitulos.Contains(c.Id)).Any());

            if (queryParams.Status.HasValue)
                query = query.Where(e => e.Status.Equals(queryParams.Status.Value));

            return await query.OrderBy(e => e.Id).Paginar(queryParams.Pagina, queryParams.Limite);
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
                        PercentJurosMulta = r.PercentJurosMulta,
                        PercentValor = r.PercentValor,
                        Status = r.Status,
                        MesAnoInicial = r.MesAnoInicial,
                        MesAnoFinal = r.MesAnoFinal,
                        ValidadeInicial = r.ValidadeInicial,
                        ValidadeFinal = r.ValidadeFinal,
                        Cursos = r.RegraNegociacaoCurso.Select(x => x.Curso),
                        Semestres = r.RegraNegociacaoSemestre.Select(x => x.Semestre),
                        SituacoesAlunos = r.RegraNegociacaoSituacaoAluno.Select(x => x.SituacaoAluno),
                        TiposPagamentos = r.RegraNegociacaoTipoPagamento.Select(x => x.TipoPagamento),
                        TiposTitulos = r.RegraNegociacaoTipoTitulo.Select(x => x.TipoTitulo)
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }

        public override Task<RegraNegociacaoModel> BuscarPorId(int id)
        {
            return DbSet.Include(r => r.RegraNegociacaoCurso)
                         .Include(r => r.RegraNegociacaoSemestre)
                         .Include(r => r.RegraNegociacaoSituacaoAluno)
                         .Include(r => r.RegraNegociacaoTipoPagamento)
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
            Db.RegraNegociacaoSemestre.RemoveRange(
                Db.RegraNegociacaoSemestre.Where(
                    c => !model.RegraNegociacaoSemestre.Select(x => x.Id).Contains(c.Id)));
            Db.RegraNegociacaoSituacaoAluno.RemoveRange(
                Db.RegraNegociacaoSituacaoAluno.Where(
                    c => !model.RegraNegociacaoSituacaoAluno.Select(x => x.Id).Contains(c.Id)));
            Db.RegraNegociacaoTipoPagamento.RemoveRange(
                Db.RegraNegociacaoTipoPagamento.Where(
                    c => !model.RegraNegociacaoTipoPagamento.Select(x => x.Id).Contains(c.Id)));
            Db.RegraNegociacaoTipoTitulo.RemoveRange(
                Db.RegraNegociacaoTipoTitulo.Where(
                    c => !model.RegraNegociacaoTipoTitulo.Select(x => x.Id).Contains(c.Id)));
                    
            return base.Alterar(model);
        }
    }
}
