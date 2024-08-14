using AIM.Dtos.EntityDtos;
using AIM.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AIM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : Controller
{
   private readonly IUnitOfWork _unitOfWork;

   public DepartmentController(IUnitOfWork unitOfWork)
   {
      _unitOfWork = unitOfWork;
   }

   [HttpPost]
   public async Task<IActionResult> CreateDepartment(DepartmentDto departmentDto)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);

      }

      var department = new Department
      {
         name = departmentDto.name,
         description = departmentDto.description
      };
      await _unitOfWork.Departments.AddAsync(department);
      await _unitOfWork.CompleteAsync();

      return CreatedAtAction(nameof(GetAllDepartments), new { id = department.id }, department);
   }
   
   
   

   [HttpGet]
   public async Task<IActionResult> GetAllDepartments()
   {
      var departments = await _unitOfWork.Departments.GetAllAsync();
      return Ok(departments);
   }

   [HttpGet("{id}")]

   public async Task<IActionResult> GetDepartmentById(int id)
   {
      var department = await _unitOfWork.Departments.GetByIdAsync(id);
      if (department == null)
      {
         return NotFound();
      }
      return Ok(department);
   }

   [HttpPut("{id}")]
   public async Task<IActionResult> UpdateDepartmentById(int id, DepartmentDto departmentDto)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      var existingDepartment = await _unitOfWork.Departments.GetByIdAsync(id);
      if (existingDepartment == null)
      {
         return NotFound();
      }
      // Map Dto to Entity
      existingDepartment.name = departmentDto.name;
      existingDepartment.description = departmentDto.description;
      
      _unitOfWork.Departments.Update(existingDepartment);
      await _unitOfWork.CompleteAsync();
      return NoContent();
   }
   
   [HttpDelete("{id}")]

   public async Task<IActionResult> DeleteById(int id)
   {
      var department = await _unitOfWork.Departments.GetByIdAsync(id);
      if (department == null)
      {
         return NotFound();

      }
      _unitOfWork.Departments.Remove(department);
      await _unitOfWork.CompleteAsync();
      return NoContent();
   }
}