using ApiFinal.Core.Entities;
using ApiFinal.Core.Repositories.Interfaces;
using ApiFinal.Data.Contexts;

namespace ApiFinal.Data.Repositories.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApiDbContext context) : base(context)
        {
            
        }
    }
}
