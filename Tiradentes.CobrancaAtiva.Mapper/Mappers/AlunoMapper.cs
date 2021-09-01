using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Tiradentes.CobrancaAtiva.Entities.Dto.Aluno;
using Tiradentes.CobrancaAtiva.Repositories.Models.Aluno;

namespace Tiradentes.CobrancaAtiva.Mapper.Mappers
{
    public class AlunoMapper : Profile
    {
        public AlunoMapper()
        {
            CreateAluno();
        }

        private void CreateAluno()
        {
            CreateMap<AlunoDto, AlunoModel>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<AlunoModel, AlunoDto>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));
        }
    }
}
