using Microsoft.EntityFrameworkCore;
using TaskManagment.Application.Interfaces;
using TaskManagment.Infrastructure.Data;

namespace TaskManagment.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T>? GetByIdAsync(int Id)
        {
            var result = await _context.FindAsync<T>(Id);
            if (result == null)
            {
                throw new InvalidOperationException("Not Found");
            }
            return result;
        }
        public async void AddAsync(T entity)
        {
            var addedEntity = (await _context.AddAsync(entity)).Entity;
            _context.SaveChanges();
        }
        public async void UpdateAsync(T entity)
        {
            var updatedEntity = _context.Update(entity).Entity;
            await _context.SaveChangesAsync();
        }
        public void DeleteAsync(int Id)
        {
            var entity = _context.Find<T>(Id);
            if (entity != null) _context.Remove(entity);
            _context.SaveChanges();
        }
    }
}