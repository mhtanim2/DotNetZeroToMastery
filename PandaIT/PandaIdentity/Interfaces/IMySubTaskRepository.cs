using PandaIdentity.Models;

namespace PandaIdentity.Interfaces
{
    public interface IMySubTaskRepository
    {

        Task<IEnumerable<MySubTask>> GetAllAsync();
        Task<MySubTask> GetAsync(Guid id);
        Task<MySubTask> AddAsync(MySubTask mySubTask);
        Task<MySubTask> DeleteAsync(Guid id);
        Task<MySubTask> UpdateAsync(Guid id, MySubTask mySubTask);
        Task<bool> IfExist(Guid id);
    }
}
