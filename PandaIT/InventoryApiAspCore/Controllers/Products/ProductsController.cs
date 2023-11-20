using AutoMapper;
using InventoryApiAspCore.Dto.Request;
using InventoryApiAspCore.Dto.Response;
using InventoryApiAspCore.Interfaces.BrandInterface;
using InventoryApiAspCore.Interfaces.CategoryInterface;
using InventoryApiAspCore.Interfaces.ProductInterface;
using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Models.Catagories;
using InventoryApiAspCore.Models.Expenses;
using InventoryApiAspCore.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace InventoryApiAspCore.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService,IBrandService brandService,
            ICategoryService categoryService,IMapper mapper)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        public async Task<IActionResult> AddTaskAsync([FromQuery] Guid BrandId, [FromQuery] Guid CategoryId,
            [FromBody] ProductCreateDto productRequest)
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
                return NotFound("User email not found in claims.");
            if (productRequest == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest();
            // Request(DTO) to Domain model
            Product ob = new Product
            {
                Name = productRequest.Name,
                UserEmail = userEmail,
                Unit = productRequest.Unit,
                Details=productRequest.Details,
                Category= await _categoryService.GetAsync(CategoryId),
                Brand=await _brandService.GetAsync(BrandId)
            };

            ob = await _productService.AddAsync(ob);
            // var getBrands = _mapper.Map<IEnumerable<BrandDto>>(ob);
            ProductDto swipe = _mapper.Map<ProductDto>(ob);

            return Ok(swipe.Name + " Expense Create Successfully");
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail == null)
                return NotFound("UserEmail not founded");
            var brands = await _productService.GetAllAsync(userEmail);
            var getBrands = _mapper.Map<IEnumerable<ProductDto>>(brands);
            return Ok(getBrands);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var ob = await _productService.GetAsync(id);
            if (ob == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDto>(ob));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            bool res = await _productService.IfExist(id);

            // Get region from the database
            if (res)
            {
                var task = await _productService.DeleteAsync(id);

                // If null NotFound
                if (task == null)
                {
                    return NotFound();
                }

                // Convert response back to DTO
                ProductDto ob = _mapper.Map<ProductDto>(task);
                return Ok(ob);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromQuery] Guid BrandId,
            [FromQuery] Guid CategoryId, [FromBody] ProductCreateDto updateRequest)
        {
            // Check if expense exists
            if (!await _productService.IfExist(id))
                return NotFound("Expense not found");

            // Validate the request model
            if (updateRequest == null || !ModelState.IsValid)
                return BadRequest();

            // Convert DTO to Domain model
            Product ob = new Product
            {
                Name = updateRequest.Name,
                Unit = updateRequest.Unit,
                Details = updateRequest.Details,
                Category = await _categoryService.GetAsync(CategoryId),
                Brand = await _brandService.GetAsync(BrandId)
            };

            // Update Region using the repository
            ob = await _productService.UpdateAsync(id, ob);

            // If Null then NotFound
            if (ob == null)
                return NotFound();

            // Convert Domain back to DTO
            ProductDto swipe = _mapper.Map<ProductDto>(ob);

            // Return Ok response
            return Ok(swipe);
        }

    }
}
