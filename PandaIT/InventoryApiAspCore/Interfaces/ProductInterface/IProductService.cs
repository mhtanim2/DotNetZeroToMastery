using InventoryApiAspCore.Models.Expenses;
using InventoryApiAspCore.Models.Products;

namespace InventoryApiAspCore.Interfaces.ProductInterface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(string userEmail);
        Task<Product> GetAsync(Guid id);
        Task<Product> AddAsync(Product product);
        Task<Product> DeleteAsync(Guid id);
        Task<Product> UpdateAsync(Guid id, Product product);
        Task<bool> IfExist(Guid id);
    }
}
