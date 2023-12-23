using ApiFinal.App.Contexts;
using ApiFinal.App.Entities;
using ApiFinal.App.Repositories.Interfaces;

namespace ApiFinal.App.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApiDbContext context) : base(context)
        {
            
        }
    }
}
