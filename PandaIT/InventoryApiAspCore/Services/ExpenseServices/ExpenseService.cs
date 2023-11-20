using InventoryApiAspCore.Data;
using InventoryApiAspCore.Interfaces.Common;
using InventoryApiAspCore.Interfaces.ExpenseInterface;
using InventoryApiAspCore.Models.Expenses;
using Microsoft.EntityFrameworkCore;

namespace InventoryApiAspCore.Services.ExpenseServices
{
    public class ExpenseService : IExpenseService
    {
        private readonly ICommonService<Expense> _commonService;
        private readonly DataContext _context;

        public ExpenseService(ICommonService<Expense> commonService,DataContext context)
        {
            _commonService= commonService;
            _context = context;
        }
        public async Task<Expense> AddAsync(Expense expense)
        {
            return await _commonService.AddAsync(expense);
        }

        public async Task<Expense> DeleteAsync(Guid id)
        {
            return await _commonService.DeleteAsync(id);
        }

        public async Task<IEnumerable<Expense>> GetAllAsync(string userEmail)
        {
            return await _commonService.GetAllAsync(userEmail);
        }

        public async Task<Expense> GetAsync(Guid id)
        {
            return await _context.Expenses.Include(i => i.ExpenseType).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> IfExist(Guid id)
        {
            return await _commonService.IfExist(id);
        }

        public async Task<bool> IsExpenseTypeAssociated(Guid expenseTypeId)
        {
            return await _context.Expenses.AnyAsync(expense => expense.ExpenseType.Id == expenseTypeId);
        }

        public async Task<Expense> UpdateAsync(Guid id, Expense expense)
        {// Check if the expense exists
            var existingExpense = await GetAsync(id);
            if (existingExpense == null)
            {
                return null;
            }
            // Update expense properties
            existingExpense.Amount = expense.Amount;
            existingExpense.Note = expense.Note;

            // Check if the expense object contains an ExpenseType
            if (expense.ExpenseType != null)
            {
                // Check if an ExpenseType with the same name already exists
                var existingExpenseType = await _context.ExpenseTypes
                    .FirstOrDefaultAsync(et => et.Name == expense.ExpenseType.Name);

                if (existingExpenseType != null)
                {
                    // Use the existing ExpenseType
                    existingExpense.ExpenseType = existingExpenseType;
                }
                else
                {
                    // Create a new ExpenseType
                    existingExpense.ExpenseType = new ExpenseType
                    {
                        Name = expense.ExpenseType.Name,
                        // Set other properties if needed
                    };
                }
            }

            existingExpense.CreatedDate = DateTime.UtcNow; // Use UTC for timestamps

            // Save changes
            await _context.SaveChangesAsync();

            return existingExpense;
        }

    }
}
