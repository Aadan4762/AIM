using AIM.Dtos.SchoolDtos;
using AIM.Interface;
using AutoMapper;

namespace AIM.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;

        public SchoolService(ISchoolRepository schoolRepository, IMapper mapper)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SchoolResponse>> GetAllSchoolsAsync()
        {
            var schools = await _schoolRepository.GetAllSchoolsAsync();
            return schools.Select(school => new SchoolResponse { IsSucceed = true, School = school });
        }

        public async Task<SchoolResponse> GetSchoolByIdAsync(Guid id)
        {
            var school = await _schoolRepository.GetSchoolByIdAsync(id);
            if (school == null)
            {
                return null;
            }
            return new SchoolResponse { IsSucceed = true, School = school };
        }

        public async Task<SchoolResponse> AddSchoolAsync(AddSchoolDto addSchoolDto)
        {
            var school = await _schoolRepository.AddSchoolAsync(addSchoolDto);
            return new SchoolResponse { IsSucceed = true, School = school };
        }

        public async Task<SchoolResponse> UpdateSchoolAsync(Guid id, UpdateSchoolDto updateSchoolDto)
        {
            var school = await _schoolRepository.UpdateSchoolAsync(id, updateSchoolDto);
            if (school == null)
            {
                return null;
            }
            return new SchoolResponse { IsSucceed = true, School = school };
        }

        public async Task<bool> DeleteSchoolAsync(Guid id)
        {
            return await _schoolRepository.DeleteSchoolAsync(id);
        }
    }
}