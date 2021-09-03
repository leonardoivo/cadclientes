using AutoMapper;

namespace Tiradentes.CobrancaAtiva.Application.AutoMapper
{
    public class AutoMapperSetup
    {
        protected AutoMapperSetup()
        { }

        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
                cfg.AddProfile(new QueryParamMappingProfile());
            });
        }
    }
}
