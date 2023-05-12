using DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace DataAccessLayer
{
    /// <summary>
    /// Realisation of the pattern "Repository"
    /// </summary>
    public class DbRepository : IDbRepository
    {
        private readonly DataContext _context;

        public DbRepository(DataContext context)
        {
            _context = context;
        }
        public IQueryable<T> GetAll<T>() where T : class, IEntity
        {
            return _context.Set<T>().AsQueryable();
        }
        public IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity
        {
            return _context.Set<T>().Where(selector).AsQueryable();
        }
        public async Task<int> Add<T>(T newEntity) where T : class, IEntity
        {
            var entity = await _context.Set<T>().AddAsync(newEntity);
            return entity.Entity.Id;
        }
        public async Task Remove<T>(T entity) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().Remove(entity));
        }
        public async Task Update<T>(T entity) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().Update(entity));
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
