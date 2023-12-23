using ApiFinal.App.Contexts;
using ApiFinal.App.Dtos.Categories;
using ApiFinal.App.Entities;
using ApiFinal.App.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFinal.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CategoryRepository _repository;

        public CategoriesController(IMapper mapper, CategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IQueryable<Category> query = await _repository.GetAllAsync();
            
            List<CategoryGetDto> categories = new List<CategoryGetDto>();

            categories = await query.Select(x => new CategoryGetDto { Name = x.Name, Description = x.Description}).ToListAsync();

            return StatusCode(200, categories);  
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Category? category = await _repository.GetByIdAsync(id);

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
            if(await _repository.IsExsist(dto.Name)) 
            {
                return StatusCode(404, new { description = $"{dto.Name} already exist" });
            }

            Category category = _mapper.Map<Category>(dto);

            await _repository.AddAsync(category);
            await _repository.SaveAsync();
            return StatusCode(201, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            Category? category = await _repository.GetByIdAsync(id);

            if (category == null)
            {
                return StatusCode(404);
            }

            _repository.Remove(category);
            await _repository.SaveAsync();
            return StatusCode(204);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CategoryUpdateDto dto)
        {
            if (await _repository.IsExsist(dto.Name, id))
            {
                return StatusCode(404, new { description = $"{dto.Name} already exist" });
            }

            Category? updatedcategory = await _repository.GetByIdAsync(id);

            if (updatedcategory == null)
            {
                return StatusCode(404);
            }

            updatedcategory.Name = dto.Name;
            updatedcategory.Description = dto.Description;
            await _repository.Update(updatedcategory);
            await _repository.SaveAsync();
            return StatusCode(204);
        }
    }
}
