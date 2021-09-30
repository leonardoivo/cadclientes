﻿using System.Threading.Tasks;
using AutoMapper;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Endereco;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _repositorio;
        protected readonly IMapper _map;

        public EnderecoService(IEnderecoRepository repositorio, IMapper map)
        {
            _map = map;
            _repositorio = repositorio;
        }

        public async Task<EnderecoViewModel> BuscarPorCep(string CEP)
        {
            EnderecoModel endereco =  await _repositorio.BuscarPorCep(CEP);

            return _map.Map<EnderecoViewModel>(endereco);
        }
    }
}
