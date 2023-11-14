using InventoryApiAspCore.Interfaces.Auth;

namespace InventoryApiAspCore.Interfaces.Common
{
    public interface ICommonService<T> where T : class, IUserEntity<object>
    {
        Task<T> AddAsync(T entity);
        Task<T> DeleteAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(string userEmail);
        Task<T> GetAsync(Guid id);
        Task<bool> IfExist(Guid id);
        Task<T> UpdateAsync(Guid id, T entity);
    }
}
