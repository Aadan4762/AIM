using AutoMapper;
using AIM.Dtos.SchoolDtos;
using AIM.Models.Entities;

namespace AIM
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddSchoolDto, School>();
            CreateMap<UpdateSchoolDto, School>();
            CreateMap<School, SchoolResponse>();
        }
    }
}