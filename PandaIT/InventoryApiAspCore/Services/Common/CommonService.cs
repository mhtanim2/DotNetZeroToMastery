using InventoryApiAspCore.Data;
using InventoryApiAspCore.Interfaces.Auth;
using InventoryApiAspCore.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace InventoryApiAspCore.Services.Common
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
            dynamic dynEntity = entity;
            dynEntity.Id = Guid.NewGuid();
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(Guid id)
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

        public async Task<IEnumerable<T>> GetAllAsync(string userEmail)
        {
            return await _context.Set<T>().Where(i => ((IUserEntity<object>)i).UserEmail == userEmail).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> IfExist(Guid id)
        {
            return await _context.Set<T>().AnyAsync(e => ((IUserEntity<object>)e).Id == id);
        }

        public async Task<T> UpdateAsync(Guid id, T entity)
        {
            var existingEntity = await GetAsync(id);
            if (existingEntity == null)
            {
                return null;
            }

            // You may need to customize this part based on your entity structure
            foreach (var property in _context.Entry(existingEntity).Properties)
            {
                if (property.Metadata.Name != "Id" && property.Metadata.Name != "UserEmail")
                {
                    property.CurrentValue = _context.Entry(entity).Property(property.Metadata.Name).CurrentValue;
                }
            }

            await _context.SaveChangesAsync();
            return existingEntity;
        }
    }
}
//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0YW5pbUBpbnZlbnRvcnkuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiV3JpdGVyIiwiZXhwIjoxNzAwMDIzNTgyLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MTE0LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMTQvIn0.KLfYacjZtpU1YLcMIALQNynD_2Dl6mG4Ww8XxIS9l4Y
