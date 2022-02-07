using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IItensGeracaoRepository : IDisposable
    {
        public bool ExisteMatricula(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela, string periodoOutros);

        public DateTime ObterDataEnvio(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela, string periodoOutros);
        Task Criar(ItensGeracaoModel model);
        Task CriarVarios(IList<ItensGeracaoModel> models);
    }
}