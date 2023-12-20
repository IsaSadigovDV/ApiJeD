using ApiFinal.App.Contexts;
using ApiFinal.App.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFinal.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CategoriesController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Category> categories = 
                await _context.Categories.ToListAsync();
            return StatusCode(200, categories);  
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Category? category = await _context.Categories.
                Where(x => x.Id == id).FirstOrDefaultAsync();

            if(category == null)
            {
                return NotFound();  
            }

            return StatusCode(200, category);
        }
    }
}
