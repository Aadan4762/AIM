using AIM.Data;
using AIM.Dtos.SchoolDtos;
using AIM.Interface;
using AIM.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIM.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly ApplicationDbContext _context;

        public SchoolRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            return await _context.Schools.ToListAsync();
        }

        public async Task<School> GetSchoolByIdAsync(Guid id)
        {
            return await _context.Schools.FindAsync(id);
        }

        public async Task<School> AddSchoolAsync(AddSchoolDto addSchoolDto)
        {
            var school = new School
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
                TitleDeed = addSchoolDto.TitleDeed
            };

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return school;
        }

        public async Task<School> UpdateSchoolAsync(Guid id, UpdateSchoolDto updateSchoolDto)
        {
            var school = await _context.Schools.FindAsync(id);
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

            _context.Schools.Update(school);
            await _context.SaveChangesAsync();

            return school;
        }

        public async Task<bool> DeleteSchoolAsync(Guid id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return false;
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
