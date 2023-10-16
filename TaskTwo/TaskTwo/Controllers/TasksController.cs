using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskTwo.Dto;
using TaskTwo.Interface;
using TaskTwo.Models;

namespace TaskTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TasksController(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tasks>))]
        [ProducesResponseType(400)]
        public IActionResult GetTask()
        {
            try{
                var result= _taskRepository.GetTasks();
                if (result == null)
                    return NotFound("Task Not founded");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id) {
            //var valid = _taskRepository.TasksExist(id);
            return Ok(_taskRepository.GetTask(id));
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201,Type=typeof(Tasks))]
        public IActionResult CreateTask([FromBody] TasksDto taskDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //Pointed
                Tasks tasks = new Tasks();
                tasks.Title = taskDto.Title;
                tasks.Description = taskDto.Description;

                _taskRepository.CreateTask(tasks);
                //showing the created task
                return CreatedAtAction(nameof(GetTask), new { id = tasks.TaskId }, tasks);
                
            }
            catch (Exception)
            {
                return StatusCode(500,"An error occurred while adding the product.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTasks(int id,
            [FromBody] TasksDto updatedTasks)
        {
            try
            {
                var upTask = _taskRepository.GetTask(id);
                if (upTask == null)
                {
                    return BadRequest("Product not fouond");
                }

                upTask.Title = updatedTasks.Title;
                upTask.Description = updatedTasks.Description;

                _taskRepository.UpdateTask(upTask);
                return Ok("Product Update Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while Updating the product.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTasks(int id)
        {
            if (!_taskRepository.TasksExist(id))
            {
                return NotFound();
            }
            var find = _taskRepository.GetTask(id);
            if (find==null)
                return NotFound();
            _taskRepository.DeleteTask(find);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }
    }
}
