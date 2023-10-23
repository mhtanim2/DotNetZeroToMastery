using Microsoft.EntityFrameworkCore;
using PandaIT.Data;
using PandaIT.Dto;
using PandaIT.Interface;
using PandaIT.Models;
using System.Threading.Tasks;

namespace PandaIT.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateDepartment(DepartmentDto departmentDto)
        {
            Department ob=new Department();
            ob.DepartmentName = departmentDto.DepartmentName;
            _context.Departments.Add(ob);
            return Save();
        }

        public bool DeleteDepartment(Department department)
        {
            _context.Remove(department);
            return Save();
        }

        public bool DepartmentExists(int depId)
        {
            return _context.Departments.Any(c => c.DepartmentId == depId);
        }

        public Department GetDepartment(int id)
        {
            return _context.Departments.Where(e => e.DepartmentId == id).Include(em=>em.Employee).FirstOrDefault();
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments.OrderBy(d=>d.DepartmentId).Include(em => em.Employee).ToList();
        }

        public Department GetDepartmentTrimToUpper(DepartmentDto departmentCreate)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDepartment(int depId, DepartmentDto departmentDto)
        {
            var obj = _context.Departments.Where(p => p.DepartmentId == depId)
                .FirstOrDefault();
            if (obj == null) return false;
            obj.DepartmentName = departmentDto.DepartmentName;
            obj.DepartmentId = depId;
            _context.Departments.Update(obj);
            return Save();
        }
    }
}
