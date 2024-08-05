using AIM.Dtos.SchoolDtos;
using AIM.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolResponse>>> GetAllSchools()
        {
            var schools = await _schoolService.GetAllSchoolsAsync();
            return Ok(schools);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolResponse>> GetSchoolById(Guid id)
        {
            var school = await _schoolService.GetSchoolByIdAsync(id);
            if (school == null)
            {
                return NotFound(new SchoolResponse { IsSucceed = false, Message = "School not found" });
            }
            return Ok(school);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SchoolResponse>> AddSchool(AddSchoolDto addSchoolDto)
        {
            var school = await _schoolService.AddSchoolAsync(addSchoolDto);
            return CreatedAtAction(nameof(GetSchoolById), new { id = school.School.Id }, school);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SchoolResponse>> UpdateSchool(Guid id, UpdateSchoolDto updateSchoolDto)
        {
            var school = await _schoolService.UpdateSchoolAsync(id, updateSchoolDto);
            if (school == null)
            {
                return NotFound(new SchoolResponse { IsSucceed = false, Message = "School not found" });
            }
            return Ok(school);
        }

        [HttpDelete("{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SchoolResponse>> DeleteSchool(Guid id)
        {
            var result = await _schoolService.DeleteSchoolAsync(id);
            if (!result)
            {
                return NotFound(new SchoolResponse { IsSucceed = false, Message = "School not found" });
            }
            return Ok(new SchoolResponse { IsSucceed = true, Message = "School deleted successfully" });
        }
    }
}
