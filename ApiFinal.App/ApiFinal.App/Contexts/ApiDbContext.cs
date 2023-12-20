using ApiFinal.App.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiFinal.App.Contexts
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options ):base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
