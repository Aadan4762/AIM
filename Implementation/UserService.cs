using AIM.Dtos.EntityDtos;
using AIM.Interface;
using AIM.Models.Entities;

namespace AIM.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        
        {
            _userRepository = userRepository;
        }
        
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            return new UserDto()
            {
                first_name = user.first_name,
                last_name = user.last_name,
                employee_no = user.employee_no,
                email = user.email,
                phone = user.phone,
                role = user.role,
                password = user.password, // Consider not exposing the password in DTO
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => new UserDto()
            {
                first_name = user.first_name,
                last_name = user.last_name,
                employee_no = user.employee_no,
                email = user.email,
                phone = user.phone,
                role = user.role,
                password = user.password, // Consider whether this should be included
            }).ToList();
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            var user = new User()
            {
                first_name = userDto.first_name,
                last_name = userDto.last_name,
                employee_no = userDto.employee_no,
                email = userDto.email,
                phone = userDto.phone,
                role = userDto.role,
                password = userDto.password, // Ensure password is hashed before storing
            };

            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(int id, UserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            user.first_name = userDto.first_name;
            user.last_name = userDto.last_name;
            user.employee_no = userDto.employee_no;
            user.email = userDto.email;
            user.phone = userDto.phone;
            user.role = userDto.role;

            if (!string.IsNullOrEmpty(userDto.password))
            {
                user.password = HashPassword(userDto.password); // Implement hashing
            }
            await _userRepository.UpdateAsync(user);
        }

        private string HashPassword(string password)
        {
            throw new NotImplementedException();
        }


        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            await _userRepository.DeleteAsync(id);
        }
    }
}
