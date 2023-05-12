using DataAccessLayer.Entities;
using System.Linq;
namespace BusinessLogic.Contracts
{
    public interface IProductService
    {
        public Task<int> CreateAsync(Product product);
        public IQueryable<Product> GetAll();
        public Task UpdateAsync(Product product);
        public Task DeleteAsync(Product product);
    }
}
