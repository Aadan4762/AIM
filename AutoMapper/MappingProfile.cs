using AIM.Dtos.SchoolDtos;
using AIM.Models.Entities;
using AutoMapper;

namespace AIM
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddSchoolDto, School>();
            CreateMap<UpdateSchoolDto, School>();
        }
    }
}