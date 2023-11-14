using AutoMapper;
using InventoryApiAspCore.Dto.Request;
using InventoryApiAspCore.Dto.Response;
using InventoryApiAspCore.Interfaces.BrandInterface;
using InventoryApiAspCore.Models.Brands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryApiAspCore.Controllers.Brands
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandsController(IBrandService brandService,IMapper mapper )
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        
        [HttpPost]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(BrandDto))]
        public async Task<IActionResult> AddTaskAsync([FromBody] BrandRequestDto brandRequestDto)
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found in claims.");
            }
            // Request(DTO) to Domain model
            Brand ob = new Brand
            {
                Name=brandRequestDto.Name,
                UserEmail=userEmail
            };

            ob=await _brandService.AddAsync(ob);
            // var getBrands = _mapper.Map<IEnumerable<BrandDto>>(ob);
            BrandDto swipe = _mapper.Map<BrandDto>(ob);

            return Ok(swipe.Name + " Brand Create Successfully");
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BrandDto>))]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var brands = await _brandService.GetAllAsync(userEmail);
            var getBrands = _mapper.Map<IEnumerable<BrandDto>>(brands);
            return Ok(getBrands);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(BrandDto))]
        public async Task<IActionResult> GetBrandAsync(Guid id) 
        {
            var ob= await _brandService.GetAsync(id);
            if (ob==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BrandDto>(ob));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            bool res = await _brandService.IfExist(id);

            // Get region from the database
            if (res)
            {
                var task = await _brandService.DeleteAsync(id);

                // If null NotFound
                if (task == null)
                {
                    return NotFound();
                }

                // Convert response back to DTO
                BrandDto ob = _mapper.Map<BrandDto>(task);
                return Ok(ob);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateTaskAsync([FromRoute] Guid id,
            [FromBody] BrandRequestDto updateRequest)
        {
            // Convert DTO to Domain model
           
            Brand brand = new Brand
            {
                Name = updateRequest.Name
            };

            // Update Region using the repository
            brand = await _brandService.UpdateAsync(id, brand);

            // If Null then NotFound
            if (brand == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            BrandDto swipe = _mapper.Map<BrandDto>(brand);
            // Return Ok response
            return Ok(swipe);
        }
    }
}
