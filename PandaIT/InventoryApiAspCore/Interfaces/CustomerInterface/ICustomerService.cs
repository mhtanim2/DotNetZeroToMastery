using InventoryApiAspCore.Models.Catagories;
using InventoryApiAspCore.Models.Customers;

namespace InventoryApiAspCore.Interfaces.CustomerInterface
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync(string userEmail);
        Task<Customer> GetAsync(Guid id);
        Task<Customer> AddAsync(Customer customer);
        Task<Customer> DeleteAsync(Guid id);
        Task<Customer> UpdateAsync(Guid id, Customer customer);
        Task<bool> IfExist(Guid id);
    }
}
