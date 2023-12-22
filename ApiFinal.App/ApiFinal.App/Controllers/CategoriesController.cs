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
                return StatusCode(404);  
            }

            return StatusCode(200, category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(_context.Categories.Any(x=>x.Name.Trim().ToLower() ==category.Name.ToLower())) 
            {
                return StatusCode(404, new { description = $"{category.Name} already exist" });
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(201, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            Category? category = await _context.Categories.
                Where(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return StatusCode(404);
            }

            _context.Remove(category);
            await _context.SaveChangesAsync();
            return StatusCode(204);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest();
            }

            if (_context.Categories.Any(x => x.Name.Trim().ToLower() == category.Name.ToLower() && x.Id !=id))
            {
                return StatusCode(404, new { description = $"{category.Name} already exist" });
            }

            Category? updatedcategory = await _context.Categories.
              Where(x => x.Id == id).FirstOrDefaultAsync();

            if (updatedcategory == null)
            {
                return StatusCode(404);
            }

            updatedcategory.Name = category.Name;
            updatedcategory.Description = category.Description;
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }
    }
}
