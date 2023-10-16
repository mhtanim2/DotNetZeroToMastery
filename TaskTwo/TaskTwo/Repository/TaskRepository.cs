using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskTwo.Data;
using TaskTwo.Dto;
using TaskTwo.Interface;
using TaskTwo.Models;

namespace TaskTwo.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TaskRepository(DataContext context,IMapper mapper)
        {
            _context=context;
            _mapper = mapper;
        }
        //get tasks list
        public IEnumerable<Tasks> GetTasks()
        {
            return _context.Tasks.OrderBy(p=>p.TaskId).Include(sub=>sub.SubTasks).ToList();
        }
        //get by id
        public Tasks GetTask(int id)
        {
            //Relational Cycle
            var result= _context.Tasks.Where(p => p.TaskId == id)
                .Include(t=> t.SubTasks)
                .FirstOrDefault();
            return result;
        }
        //get by title
        public Tasks GetTask(string Title)
        {
            var result = _context.Tasks.Where(p => p.Title== Title)
                .FirstOrDefault();
            return result;
        }

        public bool TasksExist(int id)
        {
            return _context.Tasks.Any(p => p.TaskId == id);
        }


        public bool CreateTask(Tasks task)
        {
            _context.Tasks.Add(task);
            return Save();
        }

        public bool UpdateTask(Tasks tasks)
        {
            _context.Tasks.Update(tasks);
            return Save();
        }

        public bool DeleteTask(Tasks tasks)
        {
            _context.Remove(tasks);
            return Save();
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