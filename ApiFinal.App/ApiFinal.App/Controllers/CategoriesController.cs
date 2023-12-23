using ApiFinal.App.Contexts;
using ApiFinal.App.Dtos.Categories;
using ApiFinal.App.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFinal.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //IEnumerable<Category> categories = 
            //    await _context.Categories.ToListAsync();

            IQueryable<Category> query = _context.Categories.AsQueryable();
            
            List<CategoryGetDto> categories = new List<CategoryGetDto>();

            categories = await query.Select(x => new CategoryGetDto { Name = x.Name, Description = x.Description}).ToListAsync();

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

            CategoryGetDto getDto = _mapper.Map<CategoryGetDto>(category);

            return StatusCode(200, getDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryPostDto dto)
        {
            if(_context.Categories.Any(x=>x.Name.Trim().ToLower() == dto.Name.ToLower())) 
            {
                return StatusCode(404, new { description = $"{dto.Name} already exist" });
            }

            Category category = _mapper.Map<Category>(dto);

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
        public async Task<IActionResult> Update(int id, [FromBody]CategoryUpdateDto dto)
        {
            if (_context.Categories.Any(x => x.Name.Trim().ToLower() == dto.Name.ToLower() && x.Id !=id))
            {
                return StatusCode(404, new { description = $"{dto.Name} already exist" });
            }

            Category? updatedcategory = await _context.Categories.
              Where(x => x.Id == id).FirstOrDefaultAsync();

            if (updatedcategory == null)
            {
                return StatusCode(404);
            }

            updatedcategory = _mapper.Map<Category>(updatedcategory);
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }
    }
}
