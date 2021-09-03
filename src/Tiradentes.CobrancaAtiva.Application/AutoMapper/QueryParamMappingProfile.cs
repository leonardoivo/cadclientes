using AutoMapper;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Application.AutoMapper
{
    public class QueryParamMappingProfile : Profile
    {
        public QueryParamMappingProfile()
        {
            CreateMap<ConsultaEmpresaParceiraQueryParam, EmpresaParceiraQueryParam> ();
        }
    }
}
