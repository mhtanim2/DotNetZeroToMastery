using Microsoft.EntityFrameworkCore;
using PandaIdentity.Data;
using PandaIdentity.Interfaces;
using PandaIdentity.Models;
using System.Threading.Tasks;

namespace PandaIdentity.Repository
{
    public class MySubTaskRepository: IMySubTaskRepository
    {
        private readonly DataContext _context;

        public MySubTaskRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<MySubTask> AddAsync(MySubTask mySubTask)
        {
            mySubTask.SubTaskID = Guid.NewGuid();
            mySubTask.CreatedDate = DateTime.Now;
            await _context.AddAsync(mySubTask);
            await _context.SaveChangesAsync();
            return mySubTask;
        }
               
        public async Task<MySubTask> DeleteAsync(Guid id)
        {

            var subTask = await GetAsync(id);

            if (subTask == null)
            {
                return null;
            }

            // Delete the region
            _context.MySubTasks.Remove(subTask);
            await _context.SaveChangesAsync();
            return subTask;
        }
               
        public async Task<IEnumerable<MySubTask>> GetAllAsync()
        {
            return await _context.MySubTasks.OrderBy(t => t.CreatedDate).Include(p=>p.Priority).Include(s=>s.Status).ToListAsync();
        }

        public async Task<MySubTask> GetAsync(Guid id)
        {
            return await _context.MySubTasks.FirstOrDefaultAsync(t => t.SubTaskID == id);
        }

        public async Task<bool> IfExist(Guid id)
        {
            return await _context.MySubTasks.AnyAsync(t => t.SubTaskID == id);
        }

        public async Task<MySubTask> UpdateAsync(Guid id, MySubTask mySubTask)
        {
            throw new NotImplementedException();
        }
    }
}
