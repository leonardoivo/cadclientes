using AutoMapper;
using System.Linq;
using GestaoClientes.Application.ViewModels.Cliente;
using GestaoClientes.Domain.Collections;
using GestaoClientes.Domain.Models;

namespace GestaoClientes.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteModel, ClienteViewModel>();
        }
    }
}
