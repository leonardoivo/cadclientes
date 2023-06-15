using AutoMapper;
using System.Linq;
using GestaoClientes.Application.ViewModels;
using GestaoClientes.Application.ViewModels.Cliente;
using GestaoClientes.Domain.Collections;
using GestaoClientes.Domain.DTO;
using GestaoClientes.Domain.Models;

namespace GestaoClientes.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ModelPaginada<ClienteModel>, ViewModelPaginada<ClienteViewModel>>();
            CreateMap<ClienteModel, ClienteViewModel>();
           
        }
    }
}