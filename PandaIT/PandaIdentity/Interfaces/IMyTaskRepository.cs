using PandaIdentity.Models;

namespace PandaIdentity.Interfaces
{
    public interface IMyTaskRepository
    {
        Task<IEnumerable<MyTask>> GetAllAsync();
        Task<MyTask> GetAsync(Guid id);
        Task<MyTask> AddAsync(MyTask myTask);
        Task<MyTask> DeleteAsync(Guid id);
        Task<MyTask> UpdateAsync(Guid id, MyTask myTask);
        Task<bool> IfExist(Guid id);
    }
}
