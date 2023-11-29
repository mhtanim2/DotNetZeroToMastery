using InventoryApiAspCore.Data;
using InventoryApiAspCore.Interfaces.Common;
using InventoryApiAspCore.Interfaces.ProductInterface;
using InventoryApiAspCore.Models.Expenses;
using InventoryApiAspCore.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace InventoryApiAspCore.Services.ProductServices
{
    public class ProductService:IProductService
    {
        private readonly ICommonService<Product> _commonService;
        private readonly DataContext _context;

        public ProductService(ICommonService<Product> commonService, DataContext context)
        {
            _commonService = commonService;
            _context = context;
        }

        public async Task<Product> AddAsync(Product product)
        {
            return await _commonService.AddAsync(product);
        }

        public async Task<Product> DeleteAsync(Guid id)
        {
            return await _commonService.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(string userEmail)
        {
            return await _commonService.GetAllAsync(userEmail);
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _context.Products.Include(i => i.Brand).Include(c=>c.Category).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> IfExist(Guid id)
        {
            return await _commonService.IfExist(id);
        }

        public async Task<Product> UpdateAsync(Guid id, Product product)
        {// Check if the expense exists
            var existingExpense = await GetAsync(id);
            if (existingExpense == null)
            {
                return null;
            }
            // Update expense properties
            existingExpense.Name = product.Name;
            existingExpense.Unit = product.Unit;
            existingExpense.Details = product.Details;
            

            // Check if the expense object contains an ExpenseType
            if (product.Brand != null && product.Category!=null)
            {
                // Check if an ExpenseType with the same name already exists
                var existingExpenseType = await _context.Brands
                    .FirstOrDefaultAsync(et => et.Name == product.Brand.Name);
                var CategoryType = await _context.Categories
                    .FirstOrDefaultAsync(et => et.Name == product.Category.Name);

                if (existingExpenseType != null && CategoryType!=null)
                {
                    // Use the existing ExpenseType
                    existingExpense.Brand = existingExpenseType;
                    existingExpense.Category = CategoryType;
                }
                //else
                //{
                //    // Create a new ExpenseType
                //    existingExpense.Brand = new Product
                //    {
                //        Name = product.Brand.Name,
                //        // Set other properties if needed
                //    };
                //}
            }

            existingExpense.CreatedDate = DateTime.UtcNow; // Use UTC for timestamps

            // Save changes
            await _context.SaveChangesAsync();

            return existingExpense;
        }
    }
}
