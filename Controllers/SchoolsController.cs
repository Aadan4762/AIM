using AIM.Dtos.SchoolDtos;
using AIM.Interface;
using AIM.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        // Get all schools
        [HttpGet]
       // [Authorize]
        public async Task<ActionResult<IEnumerable<School>>> GetAllSchools()
        {
            var schools = await _schoolService.GetAllSchoolsAsync();
            return Ok(schools);
        }

        // Get school by id
        [HttpGet("{id}")]
       // [Authorize]
        public async Task<ActionResult<School>> GetSchoolById(Guid id)
        {
            var school = await _schoolService.GetSchoolByIdAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            return Ok(school);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<SchoolResponse>> AddSchool([FromBody] AddSchoolDto addSchoolDto)
        {
            try
            {
                var response = await _schoolService.AddSchoolAsync(addSchoolDto);
                if (!response.IsSucceed)
                {
                    return BadRequest(new { message = "Failed to add school", details = response });
                }
                return CreatedAtAction(nameof(GetSchoolById), new { id = response.School.Id }, new { message = "School added successfully", school = response.School });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, new { message = "An internal server error occurred", details = ex.Message });
            }
        }


        // Update a school
        [HttpPut("{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SchoolResponse>> UpdateSchool(Guid id, [FromBody] UpdateSchoolDto updateSchoolDto)
        {
            var response = await _schoolService.UpdateSchoolAsync(id, updateSchoolDto);
            if (!response.IsSucceed)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // Delete a school
        [HttpDelete("{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SchoolResponse>> DeleteSchool(Guid id)
        {
            var response = await _schoolService.DeleteSchoolAsync(id);
            if (!response.IsSucceed)
            {
                return NotFound(response);
            }
            return NoContent();
        }
    }
}
