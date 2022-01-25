using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IItensGeracaoRepository : IDisposable
    {
        public bool ExisteMatricula(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela);

        public DateTime ObterDataEnvio(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela);
        Task Criar(ItensGeracaoModel model);

    }
}