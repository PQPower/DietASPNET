using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Repository pattern
    /// </summary>
    public interface IDbRepository
    {
        IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity;
        IQueryable<T> GetAll<T>() where T : class, IEntity;
        Task<int> Add<T>(T newEntity) where T : class, IEntity;
        Task Remove<T>(T entity) where T : class, IEntity;
        Task Update<T>(T entity) where T : class, IEntity;
        Task<int> SaveChangesAsync();
    }
}
