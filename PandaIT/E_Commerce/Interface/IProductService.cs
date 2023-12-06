using E_Commerce.Helper;
using E_Commerce.Models.Domain;

namespace E_Commerce.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> FilterAsync();
        Task<PagedList<Product>> GetProductsAsync(ProductParams productParams);
        Task<IEnumerable<string>> GetDistinctBrandsAsync();
        Task<IEnumerable<string>> GetDistinctTypesAsync();
        Task<Product> GetAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product> DeleteAsync(int id);
        Task<Product> UpdateAsync(int id, Product product);
        Task<bool> IfExist(int id);
        Task<bool> Save();
    }
}
