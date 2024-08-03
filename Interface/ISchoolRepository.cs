using AIM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIM.Interface
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<School>> GetAllSchoolsAsync();
        Task<School> GetSchoolByIdAsync(Guid id);
        Task<School> AddSchoolAsync(School school);
        Task<School> UpdateSchoolAsync(School school);
        Task<bool> DeleteSchoolAsync(Guid id);
    }
}