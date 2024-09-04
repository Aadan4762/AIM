using AIM.Dtos.EntityDtos;

namespace AIM.Interface;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task AddUserAsync(UserDto userDto);
    Task UpdateUserAsync(int id, UserDto userDto);
    Task DeleteUserAsync(int id);
    
}