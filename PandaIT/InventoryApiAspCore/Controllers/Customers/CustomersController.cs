﻿using AutoMapper;
using InventoryApiAspCore.Dto.Request;
using InventoryApiAspCore.Dto.Response;
using InventoryApiAspCore.Interfaces.CustomerInterface;
using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Models.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryApiAspCore.Controllers.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService,IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]
        public async Task<IActionResult> AddTaskAsync([FromBody] CustomerRequestDto customerRequest)
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return NotFound("User email not found in claims.");
            }
            // Request(DTO) to Domain model
            Customer ob = new Customer
            {
                Name = customerRequest.Name,
                UserEmail = userEmail,
                Address=customerRequest.Address,
                Phone=customerRequest.Phone,
                Email=customerRequest.Email,
            };

            ob = await _customerService.AddAsync(ob);
            // var getBrands = _mapper.Map<IEnumerable<BrandDto>>(ob);
            CustomerDto swipe = _mapper.Map<CustomerDto>(ob);

            return Ok(swipe.Name + " Customer Create Successfully");
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var brands = await _customerService.GetAllAsync(userEmail);
            var getBrands = _mapper.Map<IEnumerable<CustomerDto>>(brands);
            return Ok(getBrands);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var ob = await _customerService.GetAsync(id);
            if (ob == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CustomerDto>(ob));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            bool res = await _customerService.IfExist(id);

            // Get region from the database
            if (res)
            {
                var task = await _customerService.DeleteAsync(id);

                // If null NotFound
                if (task == null)
                {
                    return NotFound();
                }

                // Convert response back to DTO
                CustomerDto ob = _mapper.Map<CustomerDto>(task);
                return Ok(ob);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id,
            [FromBody] CustomerRequestDto updateRequest)
        {
            // Convert DTO to Domain model

            Customer brand = new Customer
            {
                Name = updateRequest.Name,
                Phone=updateRequest.Phone,
                Address=updateRequest.Address,
                Email=updateRequest.Email
            };

            // Update Region using the repository
            brand = await _customerService.UpdateAsync(id, brand);

            // If Null then NotFound
            if (brand == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            CustomerDto swipe = _mapper.Map<CustomerDto>(brand);
            // Return Ok response
            return Ok(swipe);
        }
    }
}
