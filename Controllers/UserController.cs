using AIM.Dtos.EntityDtos;
using AIM.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] string email = null,
            [FromQuery] string employeeNo = null,
            [FromQuery] string phone = null)
        {
            var usersQuery = _unitOfWork.Users.GetAllAsync();

            var users = await usersQuery;

            // Apply filtering
            if (!string.IsNullOrEmpty(email))
            {
                users = users.Where(u => u.email.Equals(email, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            if (!string.IsNullOrEmpty(employeeNo))
            {
                users = users.Where(u => u.employee_no.Equals(employeeNo, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            if (!string.IsNullOrEmpty(phone))
            {
                users = users.Where(u => u.phone.Equals(phone, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map UserDto to User entity
            var user = new User 
            {
                first_name = userDto.first_name,
                last_name = userDto.last_name,
                employee_no = userDto.employee_no,
                email = userDto.email,
                phone = userDto.phone,
                role = userDto.role,
                password = userDto.password,
                user_image = userDto.user_image
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserById(int id, UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser  = await _unitOfWork.Users.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Map UserDto to existing User entity
            existingUser.first_name = userDto.first_name;
            existingUser.last_name = userDto.last_name;
            existingUser.employee_no = userDto.employee_no;
            existingUser.email = userDto.email;
            existingUser.phone = userDto.phone;
            existingUser.role = userDto.role;
            existingUser.password = userDto.password; // Ensure to hash the password in production
            existingUser.user_image = userDto.user_image;

            // Update the existing user in the database
            _unitOfWork.Users.Update(existingUser);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
