using DataAccessLayer.Entities;

namespace BusinessLogic.Contracts
{
    public interface IUserService
    {
        public Task<int> CreateAsync(User user);
        public Task<User> GetByLoginAsync(string login);
        public Task<User> GetUserByNameAsync(string userName);
        public IQueryable<User> GetAll();
    }
}
