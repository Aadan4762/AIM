using AIM.Dtos.SchoolDtos;

namespace AIM.Interface
{
    public interface ISchoolService
    {
        Task<IEnumerable<SchoolResponse>> GetAllSchoolsAsync();
        Task<SchoolResponse> GetSchoolByIdAsync(Guid id);
        Task<SchoolResponse> AddSchoolAsync(AddSchoolDto addSchoolDto);
        Task<SchoolResponse> UpdateSchoolAsync(Guid id, UpdateSchoolDto updateSchoolDto);
        Task<bool> DeleteSchoolAsync(Guid id);
    }
}