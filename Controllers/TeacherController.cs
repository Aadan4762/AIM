using AIM.Models.Entities;
using AIM.Dtos.EntityDtos;
using Microsoft.AspNetCore.Mvc;

namespace AIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Teacher
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachers()
        {
            var teachers = await _unitOfWork.Teachers.GetAllAsync();
            var teacherDtos = new List<TeacherDto>();

            foreach (var teacher in teachers)
            {
                teacherDtos.Add(new TeacherDto
                {
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    UserName = teacher.UserName,
                    Email = teacher.Email,
                    PhoneNumber = teacher.PhoneNumber,
                    Gender = teacher.Gender
                });
            }

            return Ok(teacherDtos);
        }

        // GET: api/Teacher/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacher(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            var teacherDto = new TeacherDto
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                UserName = teacher.UserName,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                Gender = teacher.Gender
            };

            return Ok(teacherDto);
        }

        // POST: api/Teacher
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(TeacherDto teacherDto)
        {
            var teacher = new Teacher
            {
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName,
                UserName = teacherDto.UserName,
                Email = teacherDto.Email,
                PhoneNumber = teacherDto.PhoneNumber,
                Gender = teacherDto.Gender
            };

            await _unitOfWork.Teachers.AddAsync(teacher);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.id }, teacher);
        }

        // PUT: api/Teacher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, TeacherDto teacherDto)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            teacher.FirstName = teacherDto.FirstName;
            teacher.LastName = teacherDto.LastName;
            teacher.UserName = teacherDto.UserName;
            teacher.Email = teacherDto.Email;
            teacher.PhoneNumber = teacherDto.PhoneNumber;
            teacher.Gender = teacherDto.Gender;

            _unitOfWork.Teachers.Update(teacher);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/Teacher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            _unitOfWork.Teachers.Remove(teacher);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
