using ApiFinal.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiFinal.Data.Contexts
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options ):base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
