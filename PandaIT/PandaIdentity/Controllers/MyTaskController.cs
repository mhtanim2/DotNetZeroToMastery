using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaIdentity.Dto;
using PandaIdentity.Interfaces;
using PandaIdentity.Models;
using System.Threading.Tasks;

namespace PandaIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyTaskController : ControllerBase
    {
        private readonly IMyTaskRepository _myTaskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MyTaskController> _logger;

        public MyTaskController(IMyTaskRepository myTaskRepository,IMapper mapper,ILogger<MyTaskController> logger)
        {
            _myTaskRepository = myTaskRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [Authorize(Roles ="Reader,Writer")]
        public async Task<IActionResult> GetAllTasksAsync()
        {
            var tasks = await _myTaskRepository.GetAllAsync();
            var getTaskDTO = _mapper.Map<IEnumerable<MyTaskDto>>(tasks);
            return Ok(getTaskDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetTaskAsync(Guid id)
        {
            var task = await _myTaskRepository.GetAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<MyTaskDto>(task));
        }

        [HttpPost]
        [Authorize(Roles ="Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(MyTaskDto))]
        public async Task<IActionResult> AddTaskAsync(MyTaskCreateDto addTaskDto)
        {
            MyTask ob = new MyTask
            {
                Title = addTaskDto.Title,
                Description = addTaskDto.Description
            };

            // Pass details to Repository
            var result = await _myTaskRepository.AddAsync(ob);

            // Convert back to DTO
            MyTaskDto swipe = _mapper.Map<MyTaskDto>(result);
            return Ok(swipe.Title + " Created Successfully");
            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            bool res = await _myTaskRepository.IfExist(id);

                // Get region from the database
                if (res)
                {
                    var task = await _myTaskRepository.DeleteAsync(id);

                    // If null NotFound
                    if (task == null)
                    {
                        return NotFound();
                    }

                    // Convert response back to DTO
                    MyTaskDto ob = _mapper.Map<MyTaskDto>(task);
                    return Ok(ob);
                }

                return BadRequest();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateTaskAsync([FromRoute] Guid id,
            [FromBody] MyTaskCreateDto updateTaskRequest)
        {
            // Convert DTO to Domain model
                MyTask ob = new MyTask
                {
                    Title = updateTaskRequest.Title,
                    Description = updateTaskRequest.Description
                };

                // Update Region using the repository
                ob = await _myTaskRepository.UpdateAsync(id, ob);

                // If Null then NotFound
                if (ob == null)
                {
                    return NotFound();
                }

                // Convert Domain back to DTO
                MyTaskDto swipe = _mapper.Map<MyTaskDto>(ob);
                // Return Ok response
                return Ok(swipe);
            }

        }

}

