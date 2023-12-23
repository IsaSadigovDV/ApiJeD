using ApiFinal.App.Contexts;
using ApiFinal.App.Entities;

namespace ApiFinal.App.Repositories
{
    public class CategoryRepository
    {
        private readonly ApiDbContext _context;

        public CategoryRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.AddAsync(category);
        }
        public async Task<IQueryable<Category>> GetAllAsync()
        {
            return _context.Categories.AsQueryable();
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            return _context.Categories.Where(x => x.Id == id).FirstOrDefault();
        }
        public async Task Update(Category category)
        {
            _context.Update(category);
        }
        public async Task Remove(Category category)
        {
            _context.Remove(category);
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public async Task<bool> IsExsist(string name,int? id=null)
        {
            return _context.Categories.Where(x =>x.Id != id && x.Name.Trim().ToLower() == name.Trim().ToLower()).Count() > 0;
        }
    }
}
