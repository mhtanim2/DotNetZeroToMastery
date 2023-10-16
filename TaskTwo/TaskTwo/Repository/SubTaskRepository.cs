using System.Threading.Tasks;
using TaskTwo.Data;
using TaskTwo.Dto;
using TaskTwo.Interface;
using TaskTwo.Models;

namespace TaskTwo.Repository
{
    public class SubTaskRepository : ISubTaskRepository
    {
        private readonly DataContext _context;
        public SubTaskRepository(DataContext context) {
        _context = context;
        }

        public bool AddSubTask(SubTaskDto subTaskDto)
        {
            //_context.SubTasks.Add(subTaskDto);
            var result = _context.Tasks.Where(p => p.TaskId == subTaskDto.TaskId).FirstOrDefault();
            if (result == null)
                return false;
            SubTask subTask = new SubTask();
            subTask.Title = subTaskDto.Title;
            subTask.Description = subTaskDto.Description;
            subTask.CreatedDateTime = DateTime.Now;
            subTask.Task = result;
            _context.SubTasks.Add(subTask);
            return Save();
        }
        public bool UpdateSubTask(int id, SubTaskDto subTaskDto)
        {
            var _id = _context.SubTasks.Where(p => p.SubTaskId == id)
                .FirstOrDefault();
            if (_id == null) return false;
            _id.Title= subTaskDto.Title;
            _id.Description= subTaskDto.Description;
            _id.CreatedDateTime= DateTime.Now;
            _id.TaskId = subTaskDto.TaskId;
            _context.SubTasks.Update(_id);
            return Save();
        }
        public IEnumerable<SubTask> GetSubTasks()
        {
            return _context.SubTasks.OrderBy(p=>p.SubTaskId).ToList();
        }

        public SubTask GetSubTask(int id)
        {
            var result = _context.SubTasks.Where(p => p.SubTaskId == id)
                .FirstOrDefault();
            return result;
        }

        public SubTask GetSubTask(string Title)
        {
            var result = _context.SubTasks.Where(p => p.Title == Title)
                .FirstOrDefault();
            return result;
        }

        public bool SubTaskExist(int id)
        {
            return _context.SubTasks.Any(p => p.SubTaskId == id);
        }

        public bool DeleteSubTask(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
