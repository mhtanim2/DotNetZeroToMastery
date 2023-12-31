﻿using AutoMapper;
using InventoryApiAspCore.Dto.Request;
using InventoryApiAspCore.Dto.Response;
using InventoryApiAspCore.Interfaces.ExpenseInterface;
using InventoryApiAspCore.Models.Customers;
using InventoryApiAspCore.Models.Expenses;
using InventoryApiAspCore.Services.ExpenseServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryApiAspCore.Controllers.Expenses
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesTypeController : ControllerBase
    {
        private readonly IExpenseTypeService _expenseTypeService;
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;

        public ExpensesTypeController(IExpenseTypeService expenseTypeService, IExpenseService expenseService,IMapper mapper)
        {
            _expenseTypeService = expenseTypeService;
            _expenseService = expenseService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(ExpenseTypeDto))]
        public async Task<IActionResult> AddTaskAsync([FromBody] ExpenseTypeRequestDto expenseTypeRequest)
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return NotFound("User email not found in claims.");
            }
            // Request(DTO) to Domain model
            ExpenseType ob = new ExpenseType
            {
                Name = expenseTypeRequest.Name,
                UserEmail = userEmail
            };

            ob = await _expenseTypeService.AddAsync(ob);
            // var getBrands = _mapper.Map<IEnumerable<BrandDto>>(ob);
            ExpenseTypeDto swipe = _mapper.Map<ExpenseTypeDto>(ob);

            return Ok(swipe.Name + " Expense Create Successfully");
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseTypeDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var brands = await _expenseTypeService.GetAllAsync(userEmail);
            var getBrands = _mapper.Map<IEnumerable<ExpenseTypeDto>>(brands);
            return Ok(getBrands);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ExpenseTypeDto))]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var ob = await _expenseTypeService.GetAsync(id);
            if (ob == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ExpenseTypeDto>(ob));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            // Check if the ExpenseType exists
            bool exists = await _expenseTypeService.IfExist(id);

            if (exists)
            {
                // Check if the ExpenseType is associated with any Expenses
                bool isAssociated = await _expenseService.IsExpenseTypeAssociated(id);

                if (isAssociated)
                {
                    // If associated, return a BadRequest or another appropriate response
                    return BadRequest("ExpenseType is associated with Expenses. Cannot delete.");
                }

                // If not associated, proceed with deletion
                var deletedExpenseType = await _expenseTypeService.DeleteAsync(id);

                // If null, NotFound
                if (deletedExpenseType == null)
                {
                    return NotFound();
                }

                // Convert the deleted ExpenseType to DTO
                ExpenseTypeDto deletedExpenseTypeDto = _mapper.Map<ExpenseTypeDto>(deletedExpenseType);
                return Ok(deletedExpenseTypeDto);
            }

            // If the ExpenseType does not exist, return BadRequest
            return BadRequest("ExpenseType not found.");
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id,
            [FromBody] ExpenseTypeRequestDto updateRequest)
        {
            // Convert DTO to Domain model

            ExpenseType brand = new ExpenseType
            {
                Name = updateRequest.Name
            };

            // Update Region using the repository
            brand = await _expenseTypeService.UpdateAsync(id, brand);

            // If Null then NotFound
            if (brand == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            ExpenseTypeDto swipe = _mapper.Map<ExpenseTypeDto>(brand);
            // Return Ok response
            return Ok(swipe);
        }
    }
}
