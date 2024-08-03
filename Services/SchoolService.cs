using AIM.Dtos.SchoolDtos;
using AIM.Interface;
using AIM.Models.Entities;
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

        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            return await _schoolRepository.GetAllSchoolsAsync();
        }

        public async Task<School> GetSchoolByIdAsync(Guid id)
        {
            return await _schoolRepository.GetSchoolByIdAsync(id);
        }

        public async Task<SchoolResponse> AddSchoolAsync(AddSchoolDto addSchoolDto)
        {
            var school = _mapper.Map<School>(addSchoolDto);
            var result = await _schoolRepository.AddSchoolAsync(school);

            return new SchoolResponse
            {
                IsSucceed = result != null,
                Message = result != null ? "School added successfully" : "Failed to add school",
                School = result
            };
        }

        public async Task<SchoolResponse> UpdateSchoolAsync(Guid id, UpdateSchoolDto updateSchoolDto)
        {
            var school = await _schoolRepository.GetSchoolByIdAsync(id);
            if (school == null)
            {
                return new SchoolResponse
                {
                    IsSucceed = false,
                    Message = "School not found"
                };
            }

            _mapper.Map(updateSchoolDto, school);
            var result = await _schoolRepository.UpdateSchoolAsync(school);
            return new SchoolResponse
            {
                IsSucceed = result != null,
                Message = result != null ? "School updated successfully" : "Failed to update school",
                School = result
            };
        }

        public async Task<SchoolResponse> DeleteSchoolAsync(Guid id)
        {
            var result = await _schoolRepository.DeleteSchoolAsync(id);
            return new SchoolResponse
            {
                IsSucceed = result,
                Message = result ? "School deleted successfully" : "Error deleting school"
            };
        }
    }
}
