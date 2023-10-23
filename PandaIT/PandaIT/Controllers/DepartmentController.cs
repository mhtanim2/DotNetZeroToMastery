using Microsoft.AspNetCore.Mvc;
using PandaIT.Dto;
using PandaIT.Interface;
using PandaIT.Models;
using System.Threading.Tasks;

namespace PandaIT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartments()
        {
            try {
                var result = _departmentRepository.GetDepartments();
                if (result == null)
                    return NotFound("Task Not founded");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{departmentId}")]
        [ProducesResponseType(200, Type = typeof(Department))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartmentById(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            var category = _departmentRepository.GetDepartment(departmentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201, Type = typeof(DepartmentDto))]
        public IActionResult CreateSubTask(DepartmentDto departmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //showing the created task
                if (_departmentRepository.CreateDepartment(departmentDto))
                    return StatusCode(201, "Created Successfullly");
                else
                    return BadRequest("Bad request");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding the product.");
            }
        }

        [HttpPut("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTasks(int departmentId,
            [FromBody] DepartmentDto departmentDto)
        {
            try
            {
                if (_departmentRepository.UpdateDepartment(departmentId, departmentDto))
                    return StatusCode(200, "Updated Successfully");
                else
                    return BadRequest("Bad request");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while Updating the product.");
            }
        }

        [HttpDelete("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int departmentId)
        {
            try {
                if (!_departmentRepository.DepartmentExists(departmentId))
                {
                    return NotFound();
                }
                var find = _departmentRepository.GetDepartment(departmentId);
                if (find == null)
                    return NotFound();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (_departmentRepository.DeleteDepartment(find))
                    return StatusCode(200, "Updated Successfully");
                else
                    return BadRequest("Bad request");
            }
            catch(Exception) {
                return StatusCode(500, "An error occurred while Updating the product.");
            }
            
        }
    }
}
