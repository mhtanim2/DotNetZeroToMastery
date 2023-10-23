using Microsoft.AspNetCore.Mvc;
using PandaIT.Dto;
using PandaIT.Interfaces;
using PandaIT.Models;

namespace PandaIT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeDetailController : Controller
    {
        private readonly IEmployeeDetailRepository _employeeDetailRepository;

        public EmployeeDetailController(IEmployeeDetailRepository employeeDetailRepository)
        {
            _employeeDetailRepository = employeeDetailRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeDetail>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeeDetails()
        {
            try
            {
                var result = _employeeDetailRepository.GetEmployeeDetails();
                if (result == null)
                    return NotFound("EmployeeDetail Not founded");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{detailId}")]
        [ProducesResponseType(200, Type = typeof(EmployeeDetail))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeeDetailById(int detailId)
        {
            if (!_employeeDetailRepository.EmployeeDetailExists(detailId))
                return NotFound();

            var category = _employeeDetailRepository.GetEmployeeDetail(detailId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201, Type = typeof(EmployeeDetailDto))]
        public IActionResult CreateEmployeeDetail(EmployeeDetailDto employeeDetailDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //showing the created task
                if (_employeeDetailRepository.CreateEmployeeDetail(employeeDetailDto))
                    return StatusCode(201, "Created Successfullly");
                else
                    return BadRequest("Bad request");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding the product.");
            }
        }

        [HttpPut("{edId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployeeDetail(int edId,
            [FromBody] EmployeeDetailDto employeeDetailDto)
        {
            try
            {
                if (_employeeDetailRepository.UpdateEmployeeDetail(edId, employeeDetailDto))
                    return StatusCode(200, "Updated Successfully");
                else
                    return BadRequest("Bad request");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while Updating the product.");
            }
        }

        [HttpDelete("{edId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int edId)
        {
            try
            {
                if (!_employeeDetailRepository.EmployeeDetailExists(edId))
                {
                    return NotFound();
                }
                var find = _employeeDetailRepository.GetEmployeeDetail(edId);
                if (find == null)
                    return NotFound();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (_employeeDetailRepository.DeleteEmployeeDetail(find))
                    return StatusCode(200, "Updated Successfully");
                else
                    return BadRequest("Bad request");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while Updating the product.");
            }

        }
    }
}
