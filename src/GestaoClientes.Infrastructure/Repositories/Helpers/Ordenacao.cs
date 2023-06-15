using System;
using System.Linq;
using System.Linq.Expressions;

namespace GestaoClientes.Infrastructure.Repositories.Helpers
{
    public static class Ordenacao
    {
        public static IQueryable<TModel> Ordenar<TModel>(
            this IQueryable<TModel> query,
            string propriedade,
            string propriedadeDefault,
            bool desc)
        {
            var parametro = Expression.Parameter(typeof(TModel), "x");
            MemberExpression corpo;
            try
            {
                corpo = Expression.PropertyOrField(parametro, propriedade);
            }
            catch
            {
                corpo = Expression.PropertyOrField(parametro, propriedadeDefault);
            }
            var tipoModel = typeof(TModel);
            var tipoParametro = tipoModel.GetProperty(corpo.Member.Name).PropertyType;
            var expressao = Expression.Lambda(corpo, parametro);

            if (tipoParametro == typeof(string))
            {
                query = desc
                ? query.OrderByDescending((Expression<Func<TModel, string>>)expressao)
                : query.OrderBy((Expression<Func<TModel, string>>)expressao);
            }
            else if (tipoParametro == typeof(char))
            {
                query = desc
                ? query.OrderByDescending((Expression<Func<TModel, char>>)expressao)
                : query.OrderBy((Expression<Func<TModel, char>>)expressao);
            }
            else if (tipoParametro == typeof(DateTime))
            {
                query = desc
                ? query.OrderByDescending((Expression<Func<TModel, DateTime>>)expressao)
                : query.OrderBy((Expression<Func<TModel, DateTime>>)expressao);
            }
            else if (tipoParametro == typeof(int))
            {
                query = desc
                ? query.OrderByDescending((Expression<Func<TModel, int>>)expressao)
                : query.OrderBy((Expression<Func<TModel, int>>)expressao);
            }
            else if (tipoParametro == typeof(long))
            {
                query = desc
                ? query.OrderByDescending((Expression<Func<TModel, long>>)expressao)
                : query.OrderBy((Expression<Func<TModel, long>>)expressao);
            }
            else if (tipoParametro == typeof(short))
            {
                query = desc
                ? query.OrderByDescending((Expression<Func<TModel, short>>)expressao)
                : query.OrderBy((Expression<Func<TModel, short>>)expressao);
            }
            else if (tipoParametro == typeof(decimal))
            {
                query = desc
                ? query.OrderByDescending((Expression<Func<TModel, decimal>>)expressao)
                : query.OrderBy((Expression<Func<TModel, decimal>>)expressao);
            }
            else if (tipoParametro == typeof(float))
            {
                query = desc
                ? query.OrderByDescending((Expression<Func<TModel, float>>)expressao)
                : query.OrderBy((Expression<Func<TModel, float>>)expressao);
            }
            else if (tipoParametro == typeof(double))
            {
                query = desc
                ? query.OrderByDescending((Expression<Func<TModel, double>>)expressao)
                : query.OrderBy((Expression<Func<TModel, double>>)expressao);
            }
            else if (tipoParametro == typeof(bool))
            {
                query = desc
                ? query.OrderByDescending((Expression<Func<TModel, bool>>)expressao)
                : query.OrderBy((Expression<Func<TModel, bool>>)expressao);
            }
            else
            {
                query = desc
                ? query.OrderByDescending((Expression<Func<TModel, object>>)expressao)
                : query.OrderBy((Expression<Func<TModel, object>>)expressao);
            }

            return query;
        }
    }
}
