using DataAccessLayer.Entities;

namespace BusinessLogic.Contracts
{
    public interface IRoleService
    {
        public IQueryable<Role> GetAll();
    }
}
