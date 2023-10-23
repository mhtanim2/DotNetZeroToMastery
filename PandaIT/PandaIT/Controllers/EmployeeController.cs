using Microsoft.AspNetCore.Mvc;
using PandaIT.Dto;
using PandaIT.Interface;
using PandaIT.Interfaces;
using PandaIT.Models;
using PandaIT.Repository;

namespace PandaIT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeDetailRepository _employeeDetailRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository,IEmployeeDetailRepository employeeDetailRepository,IDepartmentRepository departmentRepository) 
        {
            _employeeRepository = employeeRepository;
            _employeeDetailRepository = employeeDetailRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employee = _employeeRepository.GetEmployees();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employee);
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeeById(int employeeId)
        {
            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            var employee = _employeeRepository.GetEmployee(employeeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employee);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public IActionResult AddEmployee([FromQuery] int edId, [FromQuery] int depId, EmployeeDto employeeDto)
        {
            try
            {
                // Fetch Cart by User ID
                var department = _departmentRepository.GetDepartment(depId);

                if (department == null)
                {
                    return NotFound("This departement is not exists");
                }

                // Fetch Product by ProductId
                var employeeDetail = _employeeDetailRepository.GetEmployeeDetail(edId);

                if (employeeDetail == null)
                {
                    return NotFound("Not found");
                }

                Employee employee = new Employee();
                employee.FirstName = employeeDto.FirstName;
                employee.LastName = employeeDto.LastName;           // Many to One relation
                employee.Department = department;
                employee.EmployeeDetail = employeeDetail;
                if (_employeeRepository.CreateEmployee(employee))
                    return StatusCode(201, "Created Successfullly");
                else
                    return BadRequest("Bad request");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while adding the cart item.");
            }
        }

        [HttpGet("{eId}/Project")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectByEmployee(int eId)
        {
            if (!_employeeRepository.EmployeeExists(eId))
            {
                return NotFound();
            }

            var employee = _employeeRepository.GetProjectByEmployee(eId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employee);
        }

    }
}
