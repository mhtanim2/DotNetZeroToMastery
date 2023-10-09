using TaskOne.Models;

namespace TaskOne
{
    public class Database
    {
        //We are not creating database, so we create e list to Createing tasks
        private static List<TaskModel> _tasks = new List<TaskModel>();
        
        public Task<IEnumerable<TaskModel>> GetAllTasks()
        {
            //passing with async functionalities
            return Task.FromResult<IEnumerable<TaskModel>>(_tasks);
        }

        public Task<TaskModel> GetTaskByID(int id) {
            //passing with async functionalities
            return Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));
        }

        public void AddTask(TaskModel task)
        {
            task.Id = _tasks.Count + 1;
            _tasks.Add(task);
        }
        public async Task UpdateTask(TaskModel task)
        {
            var isExist = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (isExist != null)
            {
                isExist.Tasks = task.Tasks;
                isExist.CreatedDateTime = task.CreatedDateTime;
            }
        }

        public void DeleteTask(int id)
        {
            var taskToRemove = _tasks.FirstOrDefault(t => t.Id == id);
            if (taskToRemove != null)
            {
                _tasks.Remove(taskToRemove);
            }
        }
    }
}
