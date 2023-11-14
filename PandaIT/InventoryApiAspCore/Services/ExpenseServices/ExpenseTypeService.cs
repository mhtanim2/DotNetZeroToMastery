using InventoryApiAspCore.Interfaces.Common;
using InventoryApiAspCore.Interfaces.ExpenseInterface;
using InventoryApiAspCore.Models.Customers;
using InventoryApiAspCore.Models.Expenses;

namespace InventoryApiAspCore.Services.ExpenseServices
{
    public class ExpenseTypeService:IExpenseTypeService
    {
        private readonly ICommonService<ExpenseType> _commonService;

        public ExpenseTypeService(ICommonService<ExpenseType> commonService)
        {
            _commonService = commonService;
        }

        public async Task<ExpenseType> AddAsync(ExpenseType expenseType)
        {
            return await _commonService.AddAsync(expenseType);
        }

        public async Task<ExpenseType> DeleteAsync(Guid id)
        {
            return await _commonService.DeleteAsync(id);
        }

        public async Task<IEnumerable<ExpenseType>> GetAllAsync(string userEmail)
        {
            return await _commonService.GetAllAsync(userEmail);
        }

        public async Task<ExpenseType> GetAsync(Guid id)
        {
            return await _commonService.GetAsync(id);
        }

        public async Task<bool> IfExist(Guid id)
        {
            return await _commonService.IfExist(id);
        }

        public async Task<ExpenseType> UpdateAsync(Guid id, ExpenseType expenseType)
        {
            return await _commonService.UpdateAsync(id, expenseType);
        }
    }
}
