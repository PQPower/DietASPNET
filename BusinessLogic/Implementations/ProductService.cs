using BusinessLogic.Contracts;
using DataAccessLayer;
using DataAccessLayer.Entities;

namespace BusinessLogic.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IDbRepository _db;
        public ProductService(IDbRepository db)
        {
            _db = db;
        }
        public async Task<int> CreateAsync(Product product)
        {
            var result = await _db.Add(product);
            await _db.SaveChangesAsync();

            return result;
        }
        public IQueryable<Product> GetAll()
        {
            return _db.GetAll<Product>();
        }
 
        public async Task UpdateAsync(Product product)
        {
            await _db.Update(product);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Product product)
        {
            await _db.Remove(product);
            await _db.SaveChangesAsync();
        }
    }
}
