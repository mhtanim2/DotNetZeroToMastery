using E_Commerce.Data;
using E_Commerce.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class CommonService<T> : ICommonService<T> where T : class, IUserEntity<object>

    {
        private readonly DataContext _context;

        public CommonService(DataContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {

            var entity = await GetAsync(id);
            if (entity == null)
            {
                return null;
            }
            // Delete the entity
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> IfExist(int id)
        {
            return await _context.Set<T>().AnyAsync(p=>((IUserEntity<object>)p).Id==id);
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            var existingEntity = await GetAsync(id);
            if (existingEntity == null)
            {
                return null;
            }

            // You may need to customize this part based on your entity structure
            foreach (var property in _context.Entry(existingEntity).Properties)
            {
                if (property.Metadata.Name != "Id")
                {
                    property.CurrentValue = _context.Entry(entity).Property(property.Metadata.Name).CurrentValue;
                }
            }

            await _context.SaveChangesAsync();
            return existingEntity;
        }
    }
}
