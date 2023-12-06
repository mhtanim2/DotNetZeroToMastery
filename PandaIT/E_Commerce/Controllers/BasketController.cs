using AutoMapper;
using E_Commerce.Helper;
using E_Commerce.Interface;
using E_Commerce.Models.Dto.Request;
using E_Commerce.Models.Dto.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService,IProductService productService,IMapper mapper)
        {
            _basketService = basketService;
            _productService = productService;
            _mapper = mapper;
        }
        [HttpPost]
        //[Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(BasketDto))]
        public async Task<IActionResult> AddBasketAsync([FromQuery] int productId, int quantity)
        {
            // Retrieve the basket
            var basket = await _basketService.RetriveBasket(HttpContext);

            // Add the basket
            if (basket == null) basket = await _basketService.CreateBasket(HttpContext);

            var product = await _productService.GetAsync(productId);

            if (product == null) return BadRequest(new ProblemDetails { Title = "Product not found" });

            basket.AddItem(product, quantity);

            var result = await _basketService.Save();

            if (result) return Ok(basket.MapBasketToDto());

            // Handle the case where the item could not be saved to the basket
            return BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
        }

        [HttpGet]
        // [Authorize(Roles = "Reader,Writer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BasketDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var basket = await _basketService.RetriveBasket(HttpContext);
            var getBasket = basket.MapBasketToDto(); //_mapper.Map<IEnumerable<BasketDto>>(basket);
            return Ok(new { Message = "Get Successfully", Basket = getBasket });
        }

        //[HttpGet]
        //[Route("{id:int}")]
        //// [Authorize(Roles = "Reader,Writer")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(200, Type = typeof(ProductDto))]
        //public async Task<IActionResult> GetAsync(int id)
        //{
        //    var ob = await _productService.GetAsync(id);
        //    if (ob == null)
        //        return NotFound();

        //    return Ok(_mapper.Map<ProductDto>(ob));
        //}

        [HttpDelete]
        //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteAsync(int productId, int quantity = 1)
        {
            var basket = await _basketService.RetriveBasket(HttpContext);

            if (basket == null) return NotFound();

            basket.RemoveItem(productId, quantity);

            var result=await _basketService.Save();
            if (result)
                if (result) return Ok(new {Message="Deleted Successfully", Cart_Item=basket.MapBasketToDto() });

            return BadRequest(new ProblemDetails { Title = "Problem removing item from the basket" });
        }
        

        //[HttpPut]
        //[Route("{id:int}")]
        //// [Authorize(Roles = "Writer")]
        //public async Task<IActionResult> UpdateAsync([FromRoute] int id,
        //    [FromBody] ProductRequestDto updateRequest)
        //{
        //    // Convert DTO to Domain
        //    Product ob = _mapper.Map<Product>(updateRequest);
        //    // Update Region using the repository
        //    ob = await _productService.UpdateAsync(id, ob);
        //    // If Null then NotFound
        //    if (ob == null)
        //        return NotFound();
        //    // Convert Domain back to DTO
        //    ProductDto swipe = _mapper.Map<ProductDto>(ob);
        //    // Return Ok response
        //    return Ok(swipe);
        //}
    }
}
