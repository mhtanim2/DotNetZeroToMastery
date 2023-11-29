// using InventoryApiAspCore.Interfaces.Auth;
// where T : class, IUserEntity<object>
namespace E_Commerce.Interface
{
    public interface ICommonService<T> where T : class, IUserEntity<object>
    {
        Task<T> AddAsync(T entity);
        Task<T> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<bool> IfExist(int id);
        Task<T> UpdateAsync(int id, T entity);
    }
}
