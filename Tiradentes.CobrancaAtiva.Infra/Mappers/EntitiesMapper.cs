using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tiradentes.CobrancaAtiva.Mapper.Mappers;

namespace Tiradentes.CobrancaAtiva.Infra.Mappers
{
    public class EntitiesMapper
    {
        public static void RegisterMappers(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AlunoMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
