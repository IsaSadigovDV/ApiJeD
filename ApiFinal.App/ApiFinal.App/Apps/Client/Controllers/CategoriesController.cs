using ApiFinal.Service.Dtos.Categories;
using ApiFinal.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace ApiFinal.App.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "client_v1")]


    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //_logger.LogInformation("Category GetById called by client");
            return StatusCode(200, await _categoryService.GetAllAsync());  
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //_logger.LogInformation("Category GetById called by client");
            var res = await _categoryService.GetAsync(id);
            return StatusCode(res.StatusCode, res);
        }
    }
}
