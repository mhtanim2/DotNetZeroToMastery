using InventoryApiAspCore.Models.Expenses;

namespace InventoryApiAspCore.Interfaces.Auth
{
    public interface IUserEntity<T>
    {
        Guid Id { get; set; }
        string UserEmail { get; set; }
    }
}
