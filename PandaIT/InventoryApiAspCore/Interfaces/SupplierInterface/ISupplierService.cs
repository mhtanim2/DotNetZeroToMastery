using InventoryApiAspCore.Models.Customers;
using InventoryApiAspCore.Models.Suppliers;

namespace InventoryApiAspCore.Interfaces.SupplierInterface
{
    public interface ISupplierService
    {

        Task<IEnumerable<Supplier>> GetAllAsync(string userEmail);
        Task<Supplier> GetAsync(Guid id);
        Task<Supplier> AddAsync(Supplier supplier);
        Task<Supplier> DeleteAsync(Guid id);
        Task<Supplier> UpdateAsync(Guid id, Supplier supplier);
        Task<bool> IfExist(Guid id);
    }
}
