﻿using AutoMapper;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Application.AutoMapper
{
    public class QueryParamMappingProfile : Profile
    {
        public QueryParamMappingProfile()
        {
            CreateMap<ConsultaEmpresaParceiraQueryParam, EmpresaParceiraQueryParam>();
            CreateMap<ConsultaHonorarioEmpresaParceiraQueryParam, HonorarioEmpresaParceiraQueryParam>();
            CreateMap<ConsultaInstituicaoModalidadeRegraQueryParam, InstituicaoModalidadeRegraQueryParam>();
            CreateMap<ConsultaRegraNegociacaoQueryParam, RegraNegociacaoQueryParam>();
            CreateMap<ConsultaParametroEnvioQueryParam, ParametroEnvioQueryParam>();
            CreateMap<ConsultaConflitoQueryParam, ConflitoQueryParam>();
            CreateMap<ConsultaBaixaPagamentoQueryParam, BaixaPagamentoQueryParam>();
            CreateMap<ConsultaBaixaCobrancaQueryParam, BaixaCobrancaQueryParam>();
        }
    }
}

