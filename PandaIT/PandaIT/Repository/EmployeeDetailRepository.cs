using PandaIT.Data;
using PandaIT.Dto;
using PandaIT.Interfaces;
using PandaIT.Models;

namespace PandaIT.Repository
{
    public class EmployeeDetailRepository : IEmployeeDetailRepository
    {
        private readonly DataContext _context;

        public EmployeeDetailRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateEmployeeDetail(EmployeeDetailDto employeeDetailDto)
        {
            EmployeeDetail ob = new EmployeeDetail();
            ob.Address = employeeDetailDto.Address;
            ob.ContactNumber = employeeDetailDto.ContactNumber;
            ob.Email = employeeDetailDto.Email;
            _context.EmployeeDetails.Add(ob);
            return Save();
        }

        public bool DeleteEmployeeDetail(EmployeeDetail employeeDetail)
        {
            _context.Remove(employeeDetail);
            return Save();
        }

        public bool EmployeeDetailExists(int edId)
        {
            return _context.EmployeeDetails.Any(c => c.DetailId == edId);
        }

        public EmployeeDetail GetEmployeeDetail(int id)
        {
            return _context.EmployeeDetails.Where(e => e.DetailId == id).FirstOrDefault();
        }

        public IEnumerable<EmployeeDetail> GetEmployeeDetails()
        {
            return _context.EmployeeDetails.OrderBy(d => d.DetailId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateEmployeeDetail(int edId, EmployeeDetailDto employeeDetailDto)
        {
            var obj = _context.EmployeeDetails.Where(p => p.DetailId == edId)
                .FirstOrDefault();
            EmployeeDetail ob = new EmployeeDetail();
            ob.Address = employeeDetailDto.Address;
            ob.ContactNumber = employeeDetailDto.ContactNumber;
            ob.Email = employeeDetailDto.Email;
            ob.DetailId= edId;
            _context.EmployeeDetails.Update(ob);
            return Save();
        }
    }
}
