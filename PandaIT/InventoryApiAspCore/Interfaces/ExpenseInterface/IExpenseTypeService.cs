using InventoryApiAspCore.Models.Expenses;

namespace InventoryApiAspCore.Interfaces.ExpenseInterface
{
    public interface IExpenseTypeService
    {
        Task<IEnumerable<ExpenseType>> GetAllAsync(string userEmail);
        Task<ExpenseType> GetAsync(Guid id);
        Task<ExpenseType> AddAsync(ExpenseType expenseType);
        Task<ExpenseType> DeleteAsync(Guid id);
        Task<ExpenseType> UpdateAsync(Guid id, ExpenseType expenseType);
        Task<bool> IfExist(Guid id);
    }
}