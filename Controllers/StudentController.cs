using AIM.Models.Entities;
using AIM.Dtos.EntityDtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var students = await _unitOfWork.Students.GetAllAsync();
            var studentDtos = new List<StudentDto>();

            foreach (var student in students)
            {
                studentDtos.Add(new StudentDto
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Gender = student.Gender,
                    Email = student.Email
                });
            }

            return Ok(studentDtos);
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            var studentDto = new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Gender = student.Gender,
                Email = student.Email
            };

            return Ok(studentDto);
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentDto studentDto)
        {
            var student = new Student
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Gender = studentDto.Gender,
                Email = studentDto.Email
            };

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentDto studentDto)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            student.FirstName = studentDto.FirstName;
            student.LastName = studentDto.LastName;
            student.Gender = studentDto.Gender;
            student.Email = studentDto.Email;

            _unitOfWork.Students.Update(student);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _unitOfWork.Students.Remove(student);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
