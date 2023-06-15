using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GestaoClientes.Application.QueryParams;
using GestaoClientes.Application.ViewModels.Cliente;
using GestaoClientes.Domain.Collections;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Domain.Models;
using GestaoClientes.Domain.QueryParams;
using GestaoClientes.Services.Interfaces;

namespace GestaoClientes.Services.Services
{
    public class ClienteService: IClienteService
    {
        private IClienteRepository _clienteRepository;
        protected readonly IMapper _map;

        public ClienteService(IClienteRepository clienteRepository, IMapper map)
        {
            _clienteRepository = clienteRepository;
            _map = map;
        }

        public List<ClienteViewModel> ListarClientes()
        {

            var clientes =  _clienteRepository.ListarClientes();
            return _map.Map<List<ClienteViewModel>>(clientes);

        }
        public List<ClienteViewModel> ObterClienteId(int clienteId)
        {
            var clientes = _clienteRepository.ObterClienteId(clienteId);
            return _map.Map<List<ClienteViewModel>>(clientes);
        }

        public void InserirCliente(ClienteViewModel cliente)
        {
            var clienteNovo = _map.Map<ClienteModel>(cliente);

            _clienteRepository.InserirCliente(clienteNovo);
        }

        public void AlterarCliente(ClienteViewModel cliente)
        {
            var clienteAtualizado = _map.Map<ClienteModel>(cliente);

            _clienteRepository.AlterarCliente(clienteAtualizado);

        }

        public void DeletarCliente(int clienteId)
        {

            _clienteRepository.DeletarCliente(clienteId);

        }





    }
}
