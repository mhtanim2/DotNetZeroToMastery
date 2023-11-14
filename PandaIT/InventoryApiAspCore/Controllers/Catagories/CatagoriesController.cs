using AutoMapper;
using InventoryApiAspCore.Dto.Request;
using InventoryApiAspCore.Dto.Response;
using InventoryApiAspCore.Interfaces.CategoryInterface;
using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Models.Catagories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryApiAspCore.Controllers.Catagories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CatagoriesController(ICategoryService categoryService,IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public async Task<IActionResult> AddTaskAsync([FromBody] CategoryRequestDto categoryRequest)
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return NotFound("User email not found in claims.");
            }
            // Request(DTO) to Domain model
            Category ob = new Category
            {
                Name = categoryRequest.Name,
                UserEmail = userEmail
            };

            ob = await _categoryService.AddAsync(ob);
            // var getBrands = _mapper.Map<IEnumerable<BrandDto>>(ob);
            CategoryDto swipe = _mapper.Map<CategoryDto>(ob);

            return Ok(swipe.Name + " Brand Create Successfully");
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var brands = await _categoryService.GetAllAsync(userEmail);
            var getBrands = _mapper.Map<IEnumerable<CategoryDto>>(brands);
            return Ok(getBrands);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public async Task<IActionResult> GetBrandAsync(Guid id)
        {
            var ob = await _categoryService.GetAsync(id);
            if (ob == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDto>(ob));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            bool res = await _categoryService.IfExist(id);

            // Get region from the database
            if (res)
            {
                var task = await _categoryService.DeleteAsync(id);

                // If null NotFound
                if (task == null)
                {
                    return NotFound();
                }

                // Convert response back to DTO
                CategoryDto ob = _mapper.Map<CategoryDto>(task);
                return Ok(ob);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateTaskAsync([FromRoute] Guid id,
            [FromBody] CategoryRequestDto updateRequest)
        {
            // Convert DTO to Domain model

            Category brand = new Category
            {
                Name = updateRequest.Name
            };

            // Update Region using the repository
            brand = await _categoryService.UpdateAsync(id, brand);

            // If Null then NotFound
            if (brand == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            CategoryDto swipe = _mapper.Map<CategoryDto>(brand);
            // Return Ok response
            return Ok(swipe);
        }

    }
}
