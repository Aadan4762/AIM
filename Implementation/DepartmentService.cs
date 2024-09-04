using AIM.Dtos.EntityDtos;
using AIM.Interface;
using AIM.Models.Entities;

namespace AIM.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;

        public DepartmentService(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                return null;

            return new DepartmentDto()
            {
                name = department.name,
                description = department.description
            };
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return departments.Select(department => new DepartmentDto()
            {
                name = department.name,
                description = department.description
            }).ToList();
        }

        public async Task AddDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = new Department()
            {
                name = departmentDto.name,
                description = departmentDto.description
            };
            await _departmentRepository.AddAsync(department);
        }

        public async Task UpdateDepartmentAsync(int id, DepartmentDto departmentDto)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                throw new KeyNotFoundException("Department not found");

            department.name = departmentDto.name;
            department.description = departmentDto.description;

            await _departmentRepository.UpdateAsync(department);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                throw new KeyNotFoundException("Department not found");

            await _departmentRepository.DeleteAsync(id);
        }
    }
}
