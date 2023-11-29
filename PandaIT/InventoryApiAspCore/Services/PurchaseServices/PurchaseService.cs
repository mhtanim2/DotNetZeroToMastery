using InventoryApiAspCore.Data;
using InventoryApiAspCore.Interfaces.PurchaseInterface;
using InventoryApiAspCore.Models.Purchases;
using Microsoft.EntityFrameworkCore;

namespace InventoryApiAspCore.Services.PurchaseServices
{
    public class PurchaseService : IPurchaseService
    {
        private readonly DataContext _context;

        public PurchaseService(DataContext context)
        {
            _context = context;
        }
        public async Task<Purchase> AddPurchaseWithProductsAsync(Purchase purchase, ICollection<PurchaseProduct> purchaseProducts)
        {
            // Validate inputs
            if (purchase == null || purchaseProducts == null || purchaseProducts.Count == 0)
            {
                return null;
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Add Purchase
                    await _context.Purchases.AddAsync(purchase);
                    await _context.SaveChangesAsync();

                    // Set PurchaseId for associated PurchaseProducts
                    foreach (var purchaseProduct in purchaseProducts)
                    {
                        // Have to work
                        purchaseProduct.Purchase = await GetAsync(purchase.Id);

                    }

                    // Add PurchaseProducts
                    await _context.PurchaseProducts.AddRangeAsync(purchaseProducts);
                    await _context.SaveChangesAsync();

                    // Commit transaction
                    transaction.Commit();

                    return purchase;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public async Task<Purchase> GetAsync(Guid id)
        {
            return await _context.Purchases.Include(s => s.Supplier).FirstOrDefaultAsync(p => p.Id == id);
        }
        public Task<Purchase> AddAsync(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public Task<Purchase> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Purchase>> GetAllAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IfExist(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Purchase> UpdateAsync(Guid id, Purchase purchase)
        {
            throw new NotImplementedException();
        }
    }
}
