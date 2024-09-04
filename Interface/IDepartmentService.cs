using AIM.Dtos.EntityDtos;

namespace AIM.Interface;

public interface IDepartmentService
{
    Task<DepartmentDto> GetDepartmentByIdAsync(int id);
    Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
    Task AddDepartmentAsync(DepartmentDto departmentDto);
    Task UpdateDepartmentAsync(int id, DepartmentDto departmentDto); 
    Task DeleteDepartmentAsync(int id);
}
