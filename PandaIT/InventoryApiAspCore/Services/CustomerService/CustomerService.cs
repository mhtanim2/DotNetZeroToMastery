using InventoryApiAspCore.Interfaces.Common;
using InventoryApiAspCore.Interfaces.CustomerInterface;
using InventoryApiAspCore.Models.Customers;

namespace InventoryApiAspCore.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICommonService<Customer> _commonService;

        public CustomerService(ICommonService<Customer> commonService)
        {
            _commonService = commonService;
        }
        public async Task<Customer> AddAsync(Customer customer)
        {
            return await _commonService.AddAsync(customer);
        }

        public async Task<Customer> DeleteAsync(Guid id)
        {
            return await _commonService.DeleteAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(string userEmail)
        {
            return await _commonService.GetAllAsync(userEmail);
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            return await _commonService.GetAsync(id);
        }

        public async Task<bool> IfExist(Guid id)
        {
            return await _commonService.IfExist(id);
        }

        public async Task<Customer> UpdateAsync(Guid id, Customer customer)
        {
            return await _commonService.UpdateAsync(id, customer);
        }
    }
}
