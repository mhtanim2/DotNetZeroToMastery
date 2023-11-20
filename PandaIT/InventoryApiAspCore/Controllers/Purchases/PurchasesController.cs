using AutoMapper;
using InventoryApiAspCore.Dto.Request;
using InventoryApiAspCore.Dto.Response;
using InventoryApiAspCore.Interfaces.ProductInterface;
using InventoryApiAspCore.Interfaces.PurchaseInterface;
using InventoryApiAspCore.Interfaces.SupplierInterface;
using InventoryApiAspCore.Models.Purchases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryApiAspCore.Controllers.Purchases
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;

        public PurchasesController(IPurchaseService purchaseService,
            IMapper mapper,
            IProductService productService,ISupplierService supplierService)
        {
            _purchaseService = purchaseService;
            _mapper = mapper;
            _productService = productService;
            _supplierService = supplierService;
        }

        [HttpPost]
        [Authorize(Roles = "Writer,Reader")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(PurchaseRequestDto))]
        public async Task<IActionResult> AddPurchaseWithProductsAsync([FromQuery] Guid SupplyId,
            [FromQuery] Guid ProductId,
            [FromBody] PurchaseRequestDto purchaseDto)
        {
            string userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
                return NotFound("User email not found in claims.");
            if (purchaseDto == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest();

            // Map PurchaseDto to Purchase entity
            var purchase = _mapper.Map<Purchase>(purchaseDto);
            purchase.UserEmail= userEmail;
            purchase.Supplier = await _supplierService.GetAsync(SupplyId);
            // Map PurchaseProductDtos to PurchaseProduct entities
            
            var purchaseProducts = _mapper.Map<ICollection<PurchaseProduct>>(purchaseDto.PurchaseProductRequestDto);
            foreach (var item in purchaseProducts)
            {
                item.UserEmail = userEmail;
                item.Product = await _productService.GetAsync(ProductId);
            }
            // Call the service method to add Purchase with associated PurchaseProducts
            var result = await _purchaseService.AddPurchaseWithProductsAsync(purchase, purchaseProducts);

            if (result != null)
            {
                // Map the result to DTO for response
                var resultDto = _mapper.Map<PurchaseRequestDto>(result);
                return Ok(resultDto);
            }
            else
            {
                // Handle validation error or other error cases
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Writer,Reader")]
        [Route("{id:Guid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(PurchaseRequestDto))]
        public async Task<IActionResult> GetAsync(Guid id) 
        {

            var ob = await _purchaseService.GetAsync(id);
            if (ob == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PurchaseDto>(ob));
        }
    }
}
