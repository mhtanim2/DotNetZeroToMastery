using TaskTwo.Dto;
using TaskTwo.Models;

namespace TaskTwo.Interface
{
    public interface ISubTaskRepository
    {
        IEnumerable<SubTask> GetSubTasks();
        SubTask GetSubTask(int id);
        SubTask GetSubTask(String Title);
        bool SubTaskExist(int id);
        bool AddSubTask(SubTaskDto subTaskDto);
        bool UpdateSubTask(int id,SubTaskDto subTaskDto);
        bool DeleteSubTask(int id);
        bool Save();
    }
}
