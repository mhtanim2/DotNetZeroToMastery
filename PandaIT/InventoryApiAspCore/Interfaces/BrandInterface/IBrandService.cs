using InventoryApiAspCore.Models.Brands;

namespace InventoryApiAspCore.Interfaces.BrandInterface
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllAsync(string userEmail);
        Task<Brand> GetAsync(Guid id);
        Task<Brand> AddAsync(Brand brand);
        Task<Brand> DeleteAsync(Guid id);
        Task<Brand> UpdateAsync(Guid id, Brand brand);
        Task<bool> IfExist(Guid id);
    }
}
