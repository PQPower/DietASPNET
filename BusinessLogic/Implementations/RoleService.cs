using BusinessLogic.Contracts;
using DataAccessLayer;
using DataAccessLayer.Entities;

namespace BusinessLogic.Implementations
{
    //TODO rewrite all this
    public class RoleService : IRoleService
    {
        private readonly IDbRepository _db;

        public RoleService(IDbRepository db)
        {
            _db = db;
        }
        public IQueryable<Role> GetAll()
        {
            return _db.GetAll<Role>();
        }
    }
}
