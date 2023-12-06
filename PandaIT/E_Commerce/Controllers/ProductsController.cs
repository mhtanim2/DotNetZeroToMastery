using AutoMapper;
using E_Commerce.Data;
using E_Commerce.Extenstions;
using E_Commerce.Helper;
using E_Commerce.Interface;
using E_Commerce.Models.Domain;
using E_Commerce.Models.Dto.Request;
using E_Commerce.Models.Dto.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService,IMapper mapper)
        {;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        //[Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        public async Task<IActionResult> AddTaskAsync([FromBody] ProductRequestDto requestDto)
        {
            Product ob= _mapper.Map<Product>(requestDto);
            // Request(DTO) to Domain model
            

            ob = await _productService.AddAsync(ob);
            // var getBrands = _mapper.Map<IEnumerable<BrandDto>>(ob);
            ProductDto swipe = _mapper.Map<ProductDto>(ob);

            return Ok(swipe.Name + " Customer Create Successfully");
        }
        
        [HttpGet("All")]
        // [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();
            var getBrands = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(getBrands);
        }
        
        [HttpGet]
        // [Authorize(Roles = "Reader,Writer")]
        public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery] ProductParams productParams)
        {
            var products = await _productService.GetProductsAsync(productParams);
            Response.AddPaginationHeader(products.MetaData);
            return products;
        }

        [HttpGet("{id}", Name = "GetProduct")]
        // [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        public async Task<IActionResult> GetAsync(int id)
        {
            var ob = await _productService.GetAsync(id);
            if (ob == null)
                return NotFound();
            
            return Ok(_mapper.Map<ProductDto>(ob));
        }
        
        // Getting different types of brand and type
        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var brands = await _productService.GetDistinctBrandsAsync();
            var types = await _productService.GetDistinctTypesAsync();

            return Ok(new { brands, types });
        }

        [HttpDelete]
        [Route("{id:int}")]
        // [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            bool res = await _productService.IfExist(id);
            // Get region from the database
            if (res)
            {
                var task = await _productService.DeleteAsync(id);
                // If null NotFound
                if (task == null)
                    return NotFound();
                // Convert response back to DTO
                ProductDto ob = _mapper.Map<ProductDto>(task);
                // return Ok(ob);
                return Ok(new { Message = "Deleted Product", DeletedProduct = ob });
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("{id:int}")]
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id,
            [FromBody] ProductRequestDto updateRequest)
        {
            // Convert DTO to Domain
            Product ob = _mapper.Map<Product>(updateRequest);
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
