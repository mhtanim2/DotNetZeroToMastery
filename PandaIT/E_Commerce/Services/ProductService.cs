using E_Commerce.Data;
using E_Commerce.Interface;
using E_Commerce.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class ProductService: IProductService
    {
        private readonly DataContext _context;
        private readonly ICommonService<Product> _commonService;

        public ProductService(DataContext context,ICommonService<Product> commonService)
        {
            _context = context;
            _commonService = commonService;
        }

        public async Task<Product> AddAsync(Product product)
        {
            return await _commonService.AddAsync(product);
            
        }

        public async Task<Product> DeleteAsync(int id)
        {
            return await _commonService.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _commonService.GetAllAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _commonService.GetAsync(id);
        }

        public async Task<bool> IfExist(int id)
        {
            return await _commonService.IfExist(id);
        }

        public async Task<Product> UpdateAsync(int id, Product product)
        {
            return await _commonService.UpdateAsync(id, product);

        }
    }
}
