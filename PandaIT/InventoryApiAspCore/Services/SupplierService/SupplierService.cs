using InventoryApiAspCore.Interfaces.Common;
using InventoryApiAspCore.Interfaces.SupplierInterface;
using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Models.Suppliers;
using InventoryApiAspCore.Services.Common;

namespace InventoryApiAspCore.Services.SupplierService
{
    public class SupplierService : ISupplierService
    {
        private readonly ICommonService<Supplier> _commonService;

        public SupplierService(ICommonService<Supplier> commonService)
        {
            _commonService = commonService;

        }

        public async Task<Supplier> AddAsync(Supplier supplier)
        {
            return await _commonService.AddAsync(supplier);
        }

        public async Task<Supplier> DeleteAsync(Guid id)
        {
            return await _commonService.DeleteAsync(id);
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync(string userEmail)
        {
            return await _commonService.GetAllAsync(userEmail);
        }

        public async Task<Supplier> GetAsync(Guid id)
        {
            return await _commonService.GetAsync(id);
        }

        public async Task<bool> IfExist(Guid id)
        {
            return await _commonService.IfExist(id);
        }

        public async Task<Supplier> UpdateAsync(Guid id, Supplier supplier)
        {
            return await _commonService.UpdateAsync(id, supplier);
        }
    }
}
