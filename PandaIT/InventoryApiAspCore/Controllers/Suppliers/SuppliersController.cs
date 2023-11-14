using AutoMapper;
using InventoryApiAspCore.Dto.Request;
using InventoryApiAspCore.Dto.Response;
using InventoryApiAspCore.Interfaces.SupplierInterface;
using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Models.Customers;
using InventoryApiAspCore.Models.Suppliers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryApiAspCore.Controllers.Suppliers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierService supplierService,IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(SupplierDto))]
        public async Task<IActionResult> AddTaskAsync([FromBody] SupplierRequestDto supplierRequest)
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return NotFound("User email not found in claims.");
            }
            // Request(DTO) to Domain model
            Supplier ob = new Supplier
            {
                Name = supplierRequest.Name,
                UserEmail = userEmail,
                Address = supplierRequest.Address,
                Phone = supplierRequest.Phone,
                Email = supplierRequest.Email,
            };

            ob = await _supplierService.AddAsync(ob);
            // var getBrands = _mapper.Map<IEnumerable<BrandDto>>(ob);
            SupplierDto swipe = _mapper.Map<SupplierDto>(ob);

            return Ok(swipe.Name + " Supply Create Successfully");
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SupplierDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var brands = await _supplierService.GetAllAsync(userEmail);
            var getBrands = _mapper.Map<IEnumerable<SupplierDto>>(brands);
            return Ok(getBrands);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(SupplierDto))]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var ob = await _supplierService.GetAsync(id);
            if (ob == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SupplierDto>(ob));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            bool res = await _supplierService.IfExist(id);

            // Get region from the database
            if (res)
            {
                var task = await _supplierService.DeleteAsync(id);

                // If null NotFound
                if (task == null)
                {
                    return NotFound();
                }

                // Convert response back to DTO
                SupplierDto ob = _mapper.Map<SupplierDto>(task);
                return Ok(ob);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id,
            [FromBody] SupplierRequestDto updateRequest)
        {
            // Convert DTO to Domain model

            Supplier brand = new Supplier
            {
                Name = updateRequest.Name,
                Phone = updateRequest.Phone,
                Address = updateRequest.Address,
                Email = updateRequest.Email
            };

            // Update Region using the repository
            brand = await _supplierService.UpdateAsync(id, brand);

            // If Null then NotFound
            if (brand == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            SupplierDto swipe = _mapper.Map<SupplierDto>(brand);
            // Return Ok response
            return Ok(swipe);
        }
    }
}
