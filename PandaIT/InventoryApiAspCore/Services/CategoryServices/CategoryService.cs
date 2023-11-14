using InventoryApiAspCore.Data;
using InventoryApiAspCore.Interfaces.CategoryInterface;
using InventoryApiAspCore.Interfaces.Common;
using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Models.Catagories;
using InventoryApiAspCore.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace InventoryApiAspCore.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICommonService<Category> _commonService;

        public CategoryService(ICommonService<Category> commonService)
        {
            _commonService = commonService;
        }
        public async Task<Category> AddAsync(Category category)
        {
            return await _commonService.AddAsync(category);
        }

        public async Task<Category> DeleteAsync(Guid id)
        {
            return await _commonService.DeleteAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync(string userEmail)
        {
            return await _commonService.GetAllAsync(userEmail);
        }

        public async Task<Category> GetAsync(Guid id)
        {
            return await _commonService.GetAsync(id);
        }

        public async Task<bool> IfExist(Guid id)
        {
            return await _commonService.IfExist(id);
        }

        public async Task<Category> UpdateAsync(Guid id, Category category)
        {
            return await _commonService.UpdateAsync(id, category);

        }
    }
}
