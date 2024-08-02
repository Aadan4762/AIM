using AIM.Models.Entities;

namespace AIM.Data
{
    public interface ISchoolRepository
    {
        Task AddAsync(School school);
        Task<IEnumerable<School>> GetAllAsync();
        Task<School> GetByIdAsync(Guid id);
        Task UpdateAsync(School school);
        Task<bool> DeleteAsync(Guid id);
    }
}