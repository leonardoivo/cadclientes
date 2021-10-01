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

        public async Task<IList<BuscaRegraNegociacao>> Buscar(RegraNegociacaoQueryParam queryParam)
        {
            return await  DbSet
                            .Select(r  => new BuscaRegraNegociacao {
                                Instituicao = r.Instituicao.Instituicao,
                                Modedalidade = r.Modalidade.Modalidade,
                                PercentJurosMulta = r.PercentJurosMulta,
                                PercentValor = r.PercentValor,
                                Status = r.Status,
                                Validade = r.Validade,
                                Cursos = r.RegraNegociacaoCurso.Select(x => x.Curso),
                                Semestres = r.RegraNegociacaoSemestre.Select(x => x.Semestre),
                                SituacoesAlunos = r.RegraNegociacaoSituacaoAluno.Select(x => x.SituacaoAluno),
                                TiposPagamentos = r.RegraNegociacaoTipoPagamento.Select(x => x.TipoPagamento),
                                TiposTitulos = r.RegraNegociacaoTipoTitulo.Select(x => x.TipoTitulo)
                            })
                            .ToListAsync();
        }

        public Task<BuscaRegraNegociacao> BuscarPorIdComRelacionamentos(int id)
        {
            return DbSet
                    .Where(r => r.Id.Equals(id))
                    .Select(r => new BuscaRegraNegociacao
                    {
                        Instituicao = r.Instituicao.Instituicao,
                        Modedalidade = r.Modalidade.Modalidade,
                        PercentJurosMulta = r.PercentJurosMulta,
                        PercentValor = r.PercentValor,
                        Status = r.Status,
                        Validade = r.Validade,
                        Cursos = r.RegraNegociacaoCurso.Select(x => x.Curso),
                        Semestres = r.RegraNegociacaoSemestre.Select(x => x.Semestre),
                        SituacoesAlunos = r.RegraNegociacaoSituacaoAluno.Select(x => x.SituacaoAluno),
                        TiposPagamentos = r.RegraNegociacaoTipoPagamento.Select(x => x.TipoPagamento),
                        TiposTitulos = r.RegraNegociacaoTipoTitulo.Select(x => x.TipoTitulo)
                    })
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
                         .FirstOrDefaultAsync();
        }

        public override Task Alterar(RegraNegociacaoModel model)
        {
            Db.Curso.RemoveRange(
                Db.Curso.Where(
                    c => !model.RegraNegociacaoCurso.Select(x => x.CursoId).Contains(c.Id)));
            Db.Semestre.RemoveRange(
                Db.Semestre.Where(
                    c => !model.RegraNegociacaoSemestre.Select(x => x.SemestreId).Contains(c.Id)));
            Db.SituacaoAluno.RemoveRange(
                Db.SituacaoAluno.Where(
                    c => !model.RegraNegociacaoSituacaoAluno.Select(x => x.SituacaoAlunoId).Contains(c.Id)));
            Db.TipoPagamento.RemoveRange(
                Db.TipoPagamento.Where(
                    c => !model.RegraNegociacaoTipoPagamento.Select(x => x.TipoPagamentoId).Contains(c.Id)));
            Db.TipoTitulo.RemoveRange(
                Db.TipoTitulo.Where(
                    c => !model.RegraNegociacaoTipoTitulo.Select(x => x.TipoTituloId).Contains(c.Id)));
            return base.Alterar(model);
        }
    }
}
