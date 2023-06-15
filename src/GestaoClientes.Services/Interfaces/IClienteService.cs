using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GestaoClientes.Application.QueryParams;
using GestaoClientes.Domain.Collections;
using GestaoClientes.Domain.DTO;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Domain.Models;
using GestaoClientes.Domain.QueryParams;
using GestaoClientes.Services.Interfaces;
using GestaoClientes.Application.ViewModels.Cliente;

namespace GestaoClientes.Services.Interfaces
{
    public interface IClienteService
    {
        public List<ClienteViewModel> ListarClientes();
        public List<ClienteViewModel> ObterClienteId(int clienteId);

        public void InserirCliente(ClienteViewModel cliente);

        public void AlterarCliente(ClienteViewModel cliente);

        public void DeletarCliente(int clienteId);

    }
}
