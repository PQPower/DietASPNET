using DataAccessLayer.Entities;

namespace BusinessLogic.Contracts
{
    public interface IRoleService
    {
        //TODO переделать IQueryable
        public IQueryable<Role> GetAll();
    }
}
