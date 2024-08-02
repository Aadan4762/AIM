using AIM.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIM.Data
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SchoolRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(School school)
        {
            await _dbContext.Schools.AddAsync(school);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<School>> GetAllAsync()
        {
            return await _dbContext.Schools.ToListAsync();
        }

        public async Task<School> GetByIdAsync(Guid id)
        {
            return await _dbContext.Schools.FindAsync(id);
        }

        public async Task UpdateAsync(School school)
        {
            _dbContext.Schools.Update(school);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var school = await _dbContext.Schools.FindAsync(id);
            if (school == null)
            {
                return false;
            }

            _dbContext.Schools.Remove(school);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}