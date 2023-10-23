using PandaIT.Dto;
using PandaIT.Models;

namespace PandaIT.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployeeOfAProject(int pId);
        IEnumerable<Project> GetProjectByEmployee(int eId);
        Employee GetEmployee(int id);
        bool EmployeeExists(int edId);
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(int edId, EmployeeDto employeeDto);
        bool DeleteEmployee(Employee employee);
        bool Save();
    }
}
