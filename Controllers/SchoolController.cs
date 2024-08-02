using AIM.Dtos.SchoolDtos;
using AIM.Models.Entities;
using AIM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize] // Uncomment this attribute to require authentication
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSchool(AddSchoolDto addSchoolDto)
        {
            var schoolEntity = await _schoolService.CreateSchoolAsync(addSchoolDto);
            return Ok(schoolEntity);
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAllSchools()
        {
            var allSchools = await _schoolService.GetAllSchoolsAsync();
            return Ok(allSchools);
        }

        [HttpGet]
        [Route("{id:guid}")]
      //  [Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER},{StaticUserRoles.USER}")]
        public async Task<IActionResult> GetSchoolById(Guid id)
        {
            var school = await _schoolService.GetSchoolByIdAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            return Ok(school);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateSchool(Guid id, UpdateSchoolDto updateSchoolDto)
        {
            var school = await _schoolService.UpdateSchoolAsync(id, updateSchoolDto);
            if (school == null)
            {
                return NotFound();
            }

            return Ok(school);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteSchoolById(Guid id)
        {
            var result = await _schoolService.DeleteSchoolByIdAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
