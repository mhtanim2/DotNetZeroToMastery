using InventoryApiAspCore.Models.Catagories;

namespace InventoryApiAspCore.Interfaces.CategoryInterface
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync(string userEmail);
        Task<Category> GetAsync(Guid id);
        Task<Category> AddAsync(Category category);
        Task<Category> DeleteAsync(Guid id);
        Task<Category> UpdateAsync(Guid id, Category category);
        Task<bool> IfExist(Guid id);
    }
}
