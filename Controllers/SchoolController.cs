using AIM.Data;
using AIM.Dtos.SchoolDtos;
using AIM.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Uncomment this attribute to require authentication
    public class SchoolController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public SchoolController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateSchool(AddSchoolDto addSchoolDto)
        {
            var schoolEntity = new School()
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
                TitleDeed = addSchoolDto.TitleDeed,
            };
            dbContext.Schools.Add(schoolEntity);
            dbContext.SaveChanges();
            return Ok(schoolEntity);
        }
        [HttpGet]
        [Route("list")]
        public IActionResult GetAllSchools()
        {
            var allSchools = dbContext.Schools.ToList();
            return Ok(allSchools);
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER},{StaticUserRoles.USER}")]
        public IActionResult GetSchoolById(Guid id)
        {
            var school = dbContext.Schools.Find(id);
            if (school == null)
            {
                return NotFound();
            }
            return Ok(school);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateSchool(Guid id, UpdateSchoolDto updateSchoolDto)
        {
            var school = dbContext.Schools.Find(id);
            if (school == null)
            {
                return NotFound();
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

            dbContext.SaveChanges();

            return Ok(school);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteSchoolById(Guid id)
        {
            var school = dbContext.Schools.Find(id);
            if (school == null)
            {
                return NotFound();
            }

            dbContext.Schools.Remove(school);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}