using AutoMapper;
using InventoryApiAspCore.Dto.Request;
using InventoryApiAspCore.Dto.Response;
using InventoryApiAspCore.Interfaces.ExpenseInterface;
using InventoryApiAspCore.Models.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryApiAspCore.Controllers.Expenses
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;
        private readonly IExpenseTypeService _expenseTypeService;

        public ExpensesController(IExpenseService expenseService,IMapper mapper,IExpenseTypeService expenseTypeService)
        {
            _expenseService = expenseService;
            _mapper = mapper;
            _expenseTypeService = expenseTypeService;
        }

        [HttpPost]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(ExpenseDto))]
        public async Task<IActionResult> AddTaskAsync([FromQuery]Guid ExpenesTypeId,[FromBody] ExpenseRequestDto expenseRequest)
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
                return NotFound("User email not found in claims.");
            if (expenseRequest == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest();
            // Request(DTO) to Domain model
            Expense ob = new Expense
            {
                Amount = expenseRequest.Amount,
                Note = expenseRequest.Note,
                UserEmail = userEmail,
                ExpenseType = await _expenseTypeService.GetAsync(ExpenesTypeId)
            };

            ob = await _expenseService.AddAsync(ob);
            // var getBrands = _mapper.Map<IEnumerable<BrandDto>>(ob);
            ExpenseDto swipe = _mapper.Map<ExpenseDto>(ob);

            return Ok(swipe.ExpenseType.Name + " Expense Create Successfully");
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var brands = await _expenseService.GetAllAsync(userEmail);
            var getBrands = _mapper.Map<IEnumerable<ExpenseDto>>(brands);
            return Ok(getBrands);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ExpenseDto))]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var ob = await _expenseService.GetAsync(id);
            if (ob == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ExpenseDto>(ob));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            bool res = await _expenseService.IfExist(id);

            // Get region from the database
            if (res)
            {
                var task = await _expenseService.DeleteAsync(id);

                // If null NotFound
                if (task == null)
                {
                    return NotFound();
                }

                // Convert response back to DTO
                ExpenseDto ob = _mapper.Map<ExpenseDto>(task);
                return Ok(ob);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromQuery] Guid ExpenesTypeId, [FromBody] ExpenseRequestDto updateRequest)
        {
            // Check if expense exists
            if (!await _expenseService.IfExist(id))
                return NotFound("Expense not found");

            // Validate the request model
            if (updateRequest == null || !ModelState.IsValid)
                return BadRequest();

            // Convert DTO to Domain model
            Expense expense = new Expense
            {
                Amount = updateRequest.Amount,
                Note = updateRequest.Note,
                ExpenseType = await _expenseTypeService.GetAsync(ExpenesTypeId)
            };

            // Update Region using the repository
            expense = await _expenseService.UpdateAsync(id, expense);

            // If Null then NotFound
            if (expense == null)
                return NotFound();

            // Convert Domain back to DTO
            ExpenseDto swipe = _mapper.Map<ExpenseDto>(expense);

            // Return Ok response
            return Ok(swipe);
        }
    }
}