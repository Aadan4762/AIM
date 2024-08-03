using AIM.Dtos.SchoolDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIM.Models.Entities;

namespace AIM.Interface
{
    public interface ISchoolService
    {
        Task<IEnumerable<School>> GetAllSchoolsAsync();
        Task<School> GetSchoolByIdAsync(Guid id);
        Task<SchoolResponse> AddSchoolAsync(AddSchoolDto addSchoolDto);
        Task<SchoolResponse> UpdateSchoolAsync(Guid id, UpdateSchoolDto updateSchoolDto);
        Task<SchoolResponse> DeleteSchoolAsync(Guid id);
    }
}