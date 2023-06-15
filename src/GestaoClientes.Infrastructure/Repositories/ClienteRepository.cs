
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Domain.Models;
using GestaoClientes.Infrastructure.Context;
using System.Collections.Generic;

namespace GestaoClientes.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        readonly GestaoClientesDbContext _context;


       public ClienteRepository(GestaoClientesDbContext context)
        {
            _context = context;
        }

        public List<ClienteModel> ListarClientes()
        {
            return _context.Cliente.FromSqlRaw($@" SELECT ClienteId, Nome, Porte 
                                                         FROM sca.Cliente").ToList();
        }
        public List<ClienteModel> ObterClienteId(int clienteId)
        {
            return _context.Cliente.FromSqlRaw($@" SELECT ClienteId, Nome, Porte 
                                                         FROM sca.Cliente
                                                       WHERE ClienteId = {clienteId}").ToList();
        }

        public void InserirCliente(ClienteModel cliente) {
          _context.Cliente.FromSqlRaw($@"insert into cliente (Nome,Cliente)values({cliente.Nome},{cliente.Porte})");

        }

        public void AlterarCliente(ClienteModel cliente)
        {
            _context.Cliente.FromSqlRaw($@"update  cliente set Nome={cliente.Nome},Porte={cliente.Porte} where clienteId={cliente.IdCliente}");

        }

        public void DeletarCliente(int clienteId)
        {

            _context.Cliente.FromSqlRaw($@"delete from cliente where clienteId={clienteId}");

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
