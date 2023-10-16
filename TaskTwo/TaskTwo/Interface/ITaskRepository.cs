using TaskTwo.Dto;
using TaskTwo.Models;
namespace TaskTwo.Interface
{
    public interface ITaskRepository
    {
        IEnumerable<Tasks> GetTasks();
        Tasks GetTask(int id);
        Tasks GetTask(String Title);
        bool TasksExist(int id);
        public bool CreateTask(Tasks task);
        bool UpdateTask(Tasks tasks);
        bool DeleteTask(Tasks tasks);
        //Tasks GetTasksTrimToUpper(TasksDto tasks);
        bool Save();
    }
}
