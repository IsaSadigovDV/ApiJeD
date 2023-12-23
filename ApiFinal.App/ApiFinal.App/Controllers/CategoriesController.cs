using ApiFinal.App.Contexts;
using ApiFinal.App.Dtos.Categories;
using ApiFinal.App.Entities;
using ApiFinal.App.Repositories.Implementations;
using ApiFinal.App.Repositories.Interfaces;
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
        private readonly ICategoryRepository _repository;

        public CategoriesController(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IQueryable<Category> query = await _repository.GetAllAsync(x => !x.IsDeleted);
            
            List<CategoryGetDto> categories = new List<CategoryGetDto>();

            categories = await query.Select(x => new CategoryGetDto { Name = x.Name, Description = x.Description}).ToListAsync();

            return StatusCode(200, categories);  
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Category? category = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);

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
            if(await _repository.IsExsist(x => x.Name == dto.Name)) 
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

            Category? category = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);

            if (category == null)
            {
                return StatusCode(404);
            }

            category.IsDeleted = true;
            _repository.Update(category);
            await _repository.SaveAsync();
            return StatusCode(204);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CategoryUpdateDto dto)
        {
            if (await _repository.IsExsist(x=> x.Id==id && x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return StatusCode(404, new { description = $"{dto.Name} already exist" });
            }

            Category? updatedcategory = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);

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
