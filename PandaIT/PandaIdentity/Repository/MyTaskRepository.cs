using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PandaIdentity.Data;
using PandaIdentity.Interfaces;
using PandaIdentity.Models;

namespace PandaIdentity.Repository
{
    public class MyTaskRepository: IMyTaskRepository
    {
        private readonly DataContext _context;

        public MyTaskRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<MyTask> AddAsync(MyTask myTask)
        {
            myTask.TaskID = Guid.NewGuid();
            myTask.CreatedDate= DateTime.Now;
            await _context.AddAsync(myTask);
            await _context.SaveChangesAsync();
            return myTask;
        }

        public async Task<MyTask> DeleteAsync(Guid id)
        {
            var task = await GetAsync(id);

            if (task == null)
            {
                return null;
            }

            // Delete the region
            _context.MyTasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<IEnumerable<MyTask>> GetAllAsync()
        {
            return await _context.MyTasks.OrderBy(t=>t.CreatedDate).Include(s=>s.MySubTask).ToListAsync();
        }
        public async Task<MyTask> GetAsync(Guid id)
        {
            return await _context.MyTasks
        .Include(t => t.MySubTask)
        .ThenInclude(subtask => subtask.Status)
        .Include(t => t.MySubTask)
        .ThenInclude(subtask => subtask.Priority)
        .FirstOrDefaultAsync(t => t.TaskID == id);
        }

        public async Task<bool> IfExist(Guid id)
        {
            return await _context.MyTasks.AnyAsync(t=>t.TaskID == id);
        }

        public async Task<MyTask> UpdateAsync(Guid id, MyTask myTask)
        {

            var task = await GetAsync(id);
            if (task == null)
            {
                return null;
            }
            task.Title = myTask.Title;
            task.Description = myTask.Description;
            task.CreatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
