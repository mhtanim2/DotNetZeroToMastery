using InventoryApiAspCore.Models.Expenses;

namespace InventoryApiAspCore.Interfaces.ExpenseInterface
{
    public interface IExpenseService
    {

        Task<IEnumerable<Expense>> GetAllAsync(string userEmail);
        Task<Expense> GetAsync(Guid id);
        Task<Expense> AddAsync(Expense expense);
        Task<Expense> DeleteAsync(Guid id);
        Task<Expense> UpdateAsync(Guid id, Expense expense);
        Task<bool> IfExist(Guid id);
    }
}
