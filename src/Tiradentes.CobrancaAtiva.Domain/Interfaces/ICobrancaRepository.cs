using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface ICobrancaRepository
    {
        Task<RespostasCollection> Criar(RespostasCollection model);

        Task<ICollection<RespostasCollection>> Listar(BaixaPagamentoQueryParam queryParam);
        Task<ICollection<RespostasCollection>> ListarFiltroPorMatricula(string matricula);
        Task<ICollection<RespostasCollection>> ListarFiltroPorAluno(string aluno);
        Task<ICollection<RespostasCollection>> ListarFiltroPorCpf(string cpf);
        Task<ICollection<RespostasCollection>> ListarFiltroPorAcordo(string acordo);
        Task<List<RespostasCollection>> Buscar(Expression<Func<RespostasCollection, bool>> query);
        Task<RespostasCollection> AlterarStatus(RespostasCollection model);
    }
}
