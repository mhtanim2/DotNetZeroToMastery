using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskOne;
using TaskOne.Models;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly Database _taskService;

    public TasksController(Database taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [ProducesResponseType(200,Type=typeof(IEnumerable<TaskModel>))]
    [ProducesResponseType(500, Type = typeof(String))]
    public async Task<IActionResult> Get()
    {
        try {
            var res = await _taskService.GetAllTasks();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(TaskModel))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500, Type = typeof(String))]
    public async Task<IActionResult> Get(int id)
    {
        try {
            var task = await _taskService.GetTaskByID(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);

        }
        catch (Exception ex) {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(201,Type=typeof(TaskModel))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(500, Type = typeof(string))]
    public IActionResult Post([FromBody] TaskModel task)
    {
        try
        {
            _taskService.AddTask(task);
            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it appropriately
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500, Type = typeof(string))]
    public IActionResult Put(int id, [FromBody] TaskModel updatedTask)
    {
        try {
            var existingTask = _taskService.GetTaskByID(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            _taskService.UpdateTask(updatedTask);
            return NoContent();
        }
        catch (Exception ex) {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }

    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500, Type = typeof(string))]
    public async Task<IActionResult> Delete(int id)
    {
        try {
            var task = await _taskService.GetTaskByID(id);
            if (task == null)
            {
                return NotFound();
            }

            _taskService.DeleteTask(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }

    }
}
