using E_Commerce.Data;
using E_Commerce.Extenstions;
using E_Commerce.Helper;
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

        public async Task<PagedList<Product>> GetProductsAsync(ProductParams productParams)
        {
            var query = _context.Products
                .Sort(productParams.OrderBy)
                .Search(productParams.SearchTerm)
                .Filter(productParams.Brands, productParams.Types)
                .AsQueryable();
            var products = await PagedList<Product>.ToPagedList(query, productParams.PageNumber,
                productParams.PageSize);

            return products;
        }

        public async Task<IEnumerable<string>> GetDistinctBrandsAsync()
        {
            return await _context.Products.OrderBy(p => p.Brand).Select(p => p.Brand).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<string>> GetDistinctTypesAsync()
        {
            return await _context.Products.Select(p => p.Type).Distinct().ToListAsync();
        }
        public async Task<Product> AddAsync(Product product)
        {
            return await _commonService.AddAsync(product);
            
        }

        public async Task<Product> DeleteAsync(int id)
        {
            return await _commonService.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> FilterAsync()
        {
            
            throw new Exception();
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

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
