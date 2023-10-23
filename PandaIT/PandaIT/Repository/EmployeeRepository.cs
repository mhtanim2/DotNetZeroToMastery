using PandaIT.Data;
using PandaIT.Dto;
using PandaIT.Interfaces;
using PandaIT.Models;

namespace PandaIT.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            return Save();
        }

        public bool DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            return Save();
        }

        public bool EmployeeExists(int edId)
        {
            return _context.Employees.Any(e => e.EmployeeId==edId);
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.Where(e => e.EmployeeId == id).FirstOrDefault();
        }

        public IEnumerable<Employee> GetEmployeeOfAProject(int pId)
        {
            return _context.EmployeeProjects.Where(p => p.Project.ProjectId == pId).Select(o => o.Employee).ToList();
        }

        public IEnumerable<Project> GetProjectByEmployee(int eId)
        {
            return _context.EmployeeProjects.Where(p => p.Employee.EmployeeId == eId).Select(p => p.Project).ToList();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges()>0)? true:false;
        }

        public bool UpdateEmployee(int edId, EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }
    }
}
