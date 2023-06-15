using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoClientes.Domain.Models;

namespace GestaoClientes.Domain.Interfaces
{
    public interface IClienteRepository : IDisposable
    {
        public List<ClienteModel> ListarClientes();

        public List<ClienteModel> ObterClienteId(int clienteId);

        public void InserirCliente(ClienteModel cliente);

        public void AlterarCliente(ClienteModel cliente);

        public void DeletarCliente(int clienteId);




    }
}
