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
    public class ConflitoRepository : BaseRepository<ConflitoModel>, IConflitoRepository
    {
        public ConflitoRepository(CobrancaAtivaDbContext context) : base(context)
        { }

        public async Task<ModelPaginada<BuscaConflito>> Buscar(ConflitoQueryParam queryParams)
        {
            var query = DbSet
                            .Select(r => new BuscaConflito
                            {
                                Id = r.Id,
                                EmpresaParceiraTentativa = r.EmpresaPaceiraTentativa,
                                EmpresaParceiraEnvio = (EmpresaParceiraModel)r.EmpresaParceiraEnvio,
                                ModalidadeEnsino = r.ModalidadeEnsino,
                                Nomealuno = r.Nomealuno,
                                CPF = r.CPF,
                                Matricula = r.Matricula,
                                NomeLote = r.NomeLote,
                                ParcelaConflito = r.ParcelaConflito,
                                SituacaoConflito = r.SituacaoConflito,
                                ValorConflito = r.ValorConflito,
                                DataEnvio = r.DataEnvio
                                //Cursos = r.ConflitoCurso.Select(x => x.Curso),
                                //TitulosAvulsos = r.ConflitoTituloAvulso.Select(x => x.TituloAvulso),
                                //SituacoesAlunos = r.ConflitoSituacaoAluno.Select(x => x.SituacaoAluno),
                                //TiposTitulos = r.ConflitoTipoTitulo.Select(x => x.TipoTitulo)
                            })
                            .AsQueryable();

            //if (queryParams.EmpresaParceiraTentativa != 0)
            //    query = query.Where(e => e.EmpresaParceira.Id == queryParams.EmpresaParceiraId);

            //if (queryParams.InstituicaoId != 0)
            //    query = query.Where(e => e.Instituicao.Id == queryParams.InstituicaoId);

            //if (queryParams.DiaEnvio.HasValue && queryParams.DiaEnvio != 0)
            //    query = query.Where(e => e.DiaEnvio == queryParams.DiaEnvio);

            //if (queryParams.DataEnvio.HasValue)
            //    query = query.Where(e => e.DataEnvio.Day == queryParams.DataEnvio.Value.Day &&
            //        e.DataEnvio.Month == queryParams.DataEnvio.Value.Month &&
            //        e.DataEnvio.Year == queryParams.DataEnvio.Value.Year);


            //if (queryParams.CPF)
            //    query = query.Where(e => e.ValidadeFinal.Day == queryParams.ValidadeFinal.Value.Day &&
            //        e.ValidadeFinal.Month == queryParams.ValidadeFinal.Value.Month &&
            //        e.ValidadeFinal.Year == queryParams.ValidadeFinal.Value.Year);

            //if (queryParams.InadimplenciaInicial.HasValue)
            //    query = query.Where(e => e.InadimplenciaInicial.Day == queryParams.InadimplenciaInicial.Value.Day
            //        && e.InadimplenciaInicial.Month == queryParams.InadimplenciaInicial.Value.Month
            //        && e.InadimplenciaInicial.Year == queryParams.InadimplenciaInicial.Value.Year);

            //if (queryParams.InadimplenciaFinal.HasValue)
            //    query = query.Where(e => e.InadimplenciaFinal.Day == queryParams.InadimplenciaFinal.Value.Day
            //        && e.InadimplenciaFinal.Month == queryParams.InadimplenciaFinal.Value.Month
            //        && e.InadimplenciaFinal.Year == queryParams.InadimplenciaFinal.Value.Year);

            //if (queryParams.Modalidades.Length > 0)
            //    query = query.Where(e => queryParams.Modalidades.Contains(e.Modalidade.Id));

            //if (queryParams.Cursos.Length > 0)
            //    query = query.Where(e => e.Cursos.Where(c => queryParams.Cursos.Contains(c.Id)).Any());

            //if (queryParams.TitulosAvulsos.Length > 0)
            //    query = query.Where(e => e.TitulosAvulsos.Where(c => queryParams.TitulosAvulsos.Contains(c.Id)).Any());

            //if (queryParams.SituacoesAlunos.Length > 0)
            //    query = query.Where(e => e.SituacoesAlunos.Where(c => queryParams.SituacoesAlunos.Contains(c.Id)).Any());

            //if (queryParams.TiposTitulos.Length > 0)
            //    query = query.Where(e => e.TiposTitulos.Where(c => queryParams.TiposTitulos.Contains(c.Id)).Any());

            //if (queryParams.Status.HasValue)
            //    query = query.Where(e => e.Status.Equals(queryParams.Status.Value));

            query = query.Ordenar(queryParams.OrdenarPor, "DataEnvio", queryParams.SentidoOrdenacao == "desc");

            return await query.Paginar(queryParams.Pagina, queryParams.Limite);
        }
    }
}
