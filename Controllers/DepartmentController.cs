using AIM.Dtos.EntityDtos;
using Microsoft.AspNetCore.Mvc;
using AIM.Interface;


namespace AIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("health")]
        public ActionResult<string> HealthCheck()
        {
            return "API is running";
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            if (departments == null || !departments.Any())
            {
                return NotFound("No departments found.");
            }
            return Ok(new { message = "Departments retrieved successfully.", data = departments });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }
            return Ok(new { message = $"Department with ID {id} retrieved successfully.", data = department });
        }

        [HttpPost]
        public async Task<ActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
        {
            await _departmentService.AddDepartmentAsync(departmentDto);
            return Ok(new { message = "Department added successfully.", data = departmentDto });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
            try
            {
                await _departmentService.UpdateDepartmentAsync(id, departmentDto);
                return Ok(new { message = $"Department with ID {id} updated successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Department with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the department.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                await _departmentService.DeleteDepartmentAsync(id);
                return Ok(new { message = $"Department with ID {id} deleted successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Department with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the department.", error = ex.Message });
            }
        }
    }
}
