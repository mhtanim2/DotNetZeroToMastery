using PandaIT.Dto;
using PandaIT.Models;

namespace PandaIT.Interfaces
{
    public interface IEmployeeDetailRepository
    {
        IEnumerable<EmployeeDetail> GetEmployeeDetails();
        EmployeeDetail GetEmployeeDetail(int id);
        bool EmployeeDetailExists(int edId);
        bool CreateEmployeeDetail(EmployeeDetailDto employeeDetailDto);
        bool UpdateEmployeeDetail(int edId,EmployeeDetailDto employeeDetailDto);
        bool DeleteEmployeeDetail(EmployeeDetail employeeDetail);
        bool Save();
    }
}

