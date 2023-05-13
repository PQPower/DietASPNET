using BusinessLogic.Contracts;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Implementations
{
    public class UserService : IUserService
    {
        private readonly IDbRepository _db;

        public UserService(IDbRepository db)
        {
            _db = db;
        }
        public async Task<int> CreateAsync(User user)
        {
            var result = await _db.Add(user);
            await _db.SaveChangesAsync();

            return result;
        }
        public async Task<User> GetByLoginAsync(string login)
        {
            return await _db.GetAll<User>().FirstOrDefaultAsync(u => u.Login == login);
        }
        public IQueryable<User> GetAll()
        {
            return _db.GetAll<User>().Include(u => u.Role);
        }
        public async Task<User> GetUserByNameAsync(string userName)
        {
            return await _db.GetAll<User>().Include(u => u.Role).FirstOrDefaultAsync(u => u.Login == userName);
        }
    }
}
