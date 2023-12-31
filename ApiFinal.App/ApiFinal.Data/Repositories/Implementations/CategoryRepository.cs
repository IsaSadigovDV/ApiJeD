using ApiFinal.Data.Contexts;
using ApiFinal.Core.Entities;
using ApiFinal.Core.Repositories.Interfaces;

namespace ApiFinal.Data.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApiDbContext context) : base(context)
        {
            
        }
    }
}
