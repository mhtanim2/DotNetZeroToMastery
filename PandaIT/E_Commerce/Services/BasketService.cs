using Azure;
using Azure.Core;
using E_Commerce.Data;
using E_Commerce.Interface;
using E_Commerce.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly DataContext _context;
        private readonly ICommonService<Basket> _commonService;


        public BasketService(DataContext context, ICommonService<Basket> commonService)
        {
            _context = context;
            _commonService = commonService;
        }
        public async Task<Basket> AddAsync(Basket basket)
        {
            
            return await _commonService.AddAsync(basket);
        }

        
        public async Task<Basket> CreateBasket(HttpContext httpContext)
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };

            if (httpContext != null)
            {
                httpContext.Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }
            else
            {
                return null;
            }

            var basket = new Basket { BuyerId = buyerId };
            await _context.Baskets.AddAsync(basket);
            // await _context.SaveChangesAsync();  // Assuming you want to save changes to the database
            return basket;
        }


        public Task<Basket> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Basket>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Basket> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IfExist(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Basket> RetriveBasket(HttpContext httpContext)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return null;
            }

            string buyerId = httpContext.Request.Cookies["buyerId"];

            if (string.IsNullOrWhiteSpace(buyerId))
            {
                // Handle the case where the buyerId cookie is not present or empty
                return null;
            }

            return await _context.Baskets
                .Include(i => i.BasketItem)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(b => b.BuyerId == buyerId)
                .ConfigureAwait(false);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public Task<Basket> UpdateAsync(int id, Basket basket)
        {
            throw new NotImplementedException();
        }
    }
}
