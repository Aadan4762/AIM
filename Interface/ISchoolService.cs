using AIM.Dtos.SchoolDtos;
using AIM.Models.Entities;

namespace AIM.Services
{
    public interface ISchoolService
    {
        Task<School> CreateSchoolAsync(AddSchoolDto addSchoolDto);
        Task<IEnumerable<School>> GetAllSchoolsAsync();
        Task<School> GetSchoolByIdAsync(Guid id);
        Task<School> UpdateSchoolAsync(Guid id, UpdateSchoolDto updateSchoolDto);
        Task<bool> DeleteSchoolByIdAsync(Guid id);
    }
}