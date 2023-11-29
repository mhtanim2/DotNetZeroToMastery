using E_Commerce.Models.Domain;

namespace E_Commerce.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product> DeleteAsync(int id);
        Task<Product> UpdateAsync(int id, Product product);
        Task<bool> IfExist(int id);
    }
}
