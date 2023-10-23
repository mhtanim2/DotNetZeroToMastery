using PandaIT.Dto;
using PandaIT.Models;

namespace PandaIT.Interface
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(int id);
        Department GetDepartmentTrimToUpper(DepartmentDto departmentCreate);
        bool DepartmentExists(int depId);
        bool CreateDepartment(DepartmentDto departmentDto);
        bool UpdateDepartment(int depId, DepartmentDto departmentDto);
        bool DeleteDepartment(Department department);
        bool Save();
    }
}
