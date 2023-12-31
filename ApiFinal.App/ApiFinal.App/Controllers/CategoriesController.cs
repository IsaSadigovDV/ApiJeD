using ApiFinal.Service.Dtos.Categories;
using ApiFinal.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinal.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return StatusCode(200, await _categoryService.GetAllAsync());  
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _categoryService.GetAsync(id);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryPostDto dto)
        {
            var res = await _categoryService.CreateAsync(dto);
            return StatusCode(res.StatusCode, res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _categoryService.RemoveAsync(id);
            return StatusCode(res.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            var res = await _categoryService.UpdateAsync(id, dto);
            return StatusCode(res.StatusCode, res);
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
