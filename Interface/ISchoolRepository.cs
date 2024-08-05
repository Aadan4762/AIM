using AIM.Dtos.SchoolDtos;
using AIM.Models.Entities;

namespace AIM.Interface
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<School>> GetAllSchoolsAsync();
        Task<School> GetSchoolByIdAsync(Guid id);
        Task<School> AddSchoolAsync(AddSchoolDto addSchoolDto);
        Task<School> UpdateSchoolAsync(Guid id, UpdateSchoolDto updateSchoolDto);
        Task<bool> DeleteSchoolAsync(Guid id);
    }
}