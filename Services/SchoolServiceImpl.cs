using AIM.Data;
using AIM.Dtos.SchoolDtos;
using AIM.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIM.Services
{
    public class SchoolServiceImpl : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolServiceImpl(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public async Task<School> CreateSchoolAsync(AddSchoolDto addSchoolDto)
        {
            var schoolEntity = new School()
            {
                InstitutionName = addSchoolDto.InstitutionName,
                County = addSchoolDto.County,
                SubCounty = addSchoolDto.SubCounty,
                Zone = addSchoolDto.Zone,
                Code = addSchoolDto.Code,
                Category = addSchoolDto.Category,
                Size = addSchoolDto.Size,
                Longitude = addSchoolDto.Longitude,
                Latitude = addSchoolDto.Latitude,
                TitleDeed = addSchoolDto.TitleDeed,
            };
            await _schoolRepository.AddAsync(schoolEntity);
            return schoolEntity;
        }

        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            return await _schoolRepository.GetAllAsync();
        }

        public async Task<School> GetSchoolByIdAsync(Guid id)
        {
            return await _schoolRepository.GetByIdAsync(id);
        }

        public async Task<School> UpdateSchoolAsync(Guid id, UpdateSchoolDto updateSchoolDto)
        {
            var school = await _schoolRepository.GetByIdAsync(id);
            if (school == null)
            {
                return null;
            }

            school.InstitutionName = updateSchoolDto.InstitutionName;
            school.County = updateSchoolDto.County;
            school.SubCounty = updateSchoolDto.SubCounty;
            school.Zone = updateSchoolDto.Zone;
            school.Code = updateSchoolDto.Code;
            school.Category = updateSchoolDto.Category;
            school.Size = updateSchoolDto.Size;
            school.Longitude = updateSchoolDto.Longitude;
            school.Latitude = updateSchoolDto.Latitude;
            school.TitleDeed = updateSchoolDto.TitleDeed;

            await _schoolRepository.UpdateAsync(school);
            return school;
        }

        public async Task<bool> DeleteSchoolByIdAsync(Guid id)
        {
            return await _schoolRepository.DeleteAsync(id);
        }
    }
}
