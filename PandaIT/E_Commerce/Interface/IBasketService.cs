using E_Commerce.Models.Domain;

namespace E_Commerce.Interface
{
    public interface IBasketService
    {
        Task<IEnumerable<Basket>> GetAllAsync();
        Task<Basket> RetriveBasket(HttpContext httpContext);
        Task<Basket> GetAsync(int id);
        Task<Basket> AddAsync(Basket basket);
        Task<Basket> CreateBasket(HttpContext httpContext);
        Task<Basket> DeleteAsync(int id);
        Task<Basket> UpdateAsync(int id, Basket basket);
        Task<bool> IfExist(int id);
        Task<bool> Save();

    }
}
