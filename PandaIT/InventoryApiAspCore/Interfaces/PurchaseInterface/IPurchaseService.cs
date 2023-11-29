using InventoryApiAspCore.Models.Purchases;

namespace InventoryApiAspCore.Interfaces.PurchaseInterface
{
    public interface IPurchaseService
    {

        Task<IEnumerable<Purchase>> GetAllAsync(string userEmail);
        Task<Purchase> AddPurchaseWithProductsAsync(Purchase purchase, ICollection<PurchaseProduct> purchaseProducts);
        Task<Purchase> GetAsync(Guid id);
        Task<Purchase> AddAsync(Purchase purchase);
        Task<Purchase> DeleteAsync(Guid id);
        Task<Purchase> UpdateAsync(Guid id, Purchase purchase);
        Task<bool> IfExist(Guid id);
    }
}
