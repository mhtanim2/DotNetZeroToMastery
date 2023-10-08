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
    public ActionResult<IEnumerable<TaskModel>> Get()
    {
        return Ok(_taskService.GetAllTasks());
    }

    [HttpGet("{id}")]
    public ActionResult<TaskModel> Get(int id)
    {
        var task = _taskService.GetTaskByID(id);
        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpPost]
    public ActionResult<TaskModel> Post([FromBody] TaskModel task)
    {
        _taskService.AddTask(task);
        return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] TaskModel updatedTask)
    {
        var existingTask = _taskService.GetTaskByID(id);
        if (existingTask == null)
        {
            return NotFound();
        }

        _taskService.UpdateTask(updatedTask);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = _taskService.GetTaskByID(id);
        if (task == null)
        {
            return NotFound();
        }

        _taskService.DeleteTask(id);
        return NoContent();
    }
}
