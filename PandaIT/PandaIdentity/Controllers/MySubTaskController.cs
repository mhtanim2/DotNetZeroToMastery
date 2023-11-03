using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PandaIdentity.Data;
using PandaIdentity.Dto;
using PandaIdentity.Interfaces;
using PandaIdentity.Models;
using PandaIdentity.Repository;

namespace PandaIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MySubTaskController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMySubTaskRepository _mySubTaskRepository;
        private readonly IMapper _mapper;
        private readonly IMyTaskRepository _myTaskRepository;

        public MySubTaskController(DataContext context,IMySubTaskRepository mySubTaskRepository,IMapper mapper,IMyTaskRepository myTaskRepository)
        {
            _context = context;
            _mySubTaskRepository = mySubTaskRepository;
            _mapper = mapper;
            _myTaskRepository = myTaskRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(MySubTaskDto))]
        public async Task<IActionResult> AddTaskAsync([FromBody]MySubTaskCreateDto addSubTaskDto,
            [FromQuery] Guid PriorityId, [FromQuery] Guid StatusId, [FromQuery] Guid TaskId)
        {
            // Request(DTO) to Domain model
            MySubTask ob = new MySubTask
            {
                Title = addSubTaskDto.Title,
                Description = addSubTaskDto.Description,
                AssignedTo = addSubTaskDto.AssignedTo,
                DueDate = addSubTaskDto.DueDate,
                MyTask = await _myTaskRepository.GetAsync(TaskId),
                Priority= await _context.Priorities.FirstOrDefaultAsync(p => p.PriorityId==PriorityId),
                Status= await _context.Statuses.FirstOrDefaultAsync(p => p.StatusId == StatusId)
            };

            // Pass details to Repository
            var result = await _mySubTaskRepository.AddAsync(ob);

            // Convert back to DTO
            MySubTaskDto swipe = _mapper.Map<MySubTaskDto>(result);
            return Ok(swipe.Title + " Create Successfully");
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAllSubTasksAsync()
        {
            var subTask = await _mySubTaskRepository.GetAllAsync();

            var getSubTaskDTO = _mapper.Map<IEnumerable<MySubTaskDto>>(subTask);

            return Ok(getSubTaskDTO);
        }
    }
}