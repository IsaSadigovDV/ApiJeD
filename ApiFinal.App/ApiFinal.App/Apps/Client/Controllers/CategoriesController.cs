using ApiFinal.Service.Dtos.Categories;
using ApiFinal.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace ApiFinal.App.Client.Controllers
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
    }
}
