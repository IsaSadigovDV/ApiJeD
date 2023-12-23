using ApiFinal.App.Contexts;
using ApiFinal.App.Dtos.Categories;
using ApiFinal.App.Entities;
using ApiFinal.App.Repositories.Implementations;
using ApiFinal.App.Repositories.Interfaces;
using ApiFinal.App.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Update(int id, [FromBody]CategoryUpdateDto dto)
        {
            var res = await _categoryService.UpdateAsync(id, dto);
            return StatusCode(res.StatusCode, res);
        }
    }
}
