using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskTwo.Dto;
using TaskTwo.Interface;
using TaskTwo.Models;
using TaskTwo.Repository;

namespace TaskTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTaskController : Controller
    {
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly IMapper _mapper;

        public SubTaskController(ISubTaskRepository subTaskRepository,IMapper mapper)
        {
            _subTaskRepository = subTaskRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateSubTask( SubTaskDto subTaskDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //showing the created task
                if (_subTaskRepository.AddSubTask(subTaskDto))
                    return StatusCode(201, "Created Successfullly");
                else
                    return BadRequest("Bad request");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding the product.");
            }
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTasks(int id,
            [FromBody] SubTaskDto updateSubTask)
        {
            try
            {
                if (_subTaskRepository.UpdateSubTask(id, updateSubTask))
                    return StatusCode(200, "Updated Successfully");
                else
                    return BadRequest("Bad request");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while Updating the product.");
            }
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SubTask>))]
        public IActionResult GetSubTask()
        {
            try
            {
                var result = _subTaskRepository.GetSubTasks();
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
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(SubTask))]
        public IActionResult GetTask(int id)
        {
            try
            {
                var valid = _subTaskRepository.SubTaskExist(id);
                if (!valid)
                    return NotFound("No id founded");
                var result = _mapper.Map<TasksDto>(_subTaskRepository.GetSubTask(id));
                if (result == null)
                    return NotFound("Not foundede");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

    }
}