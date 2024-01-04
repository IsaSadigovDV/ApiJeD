using ApiFinal.Service.Dtos.Categories;
using ApiFinal.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ApiFinal.App.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    [ApiExplorerSettings(GroupName = "admin_v1")]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Category GetAll called by Admin");
            return StatusCode(200, await _categoryService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Category GetById called by Admin");
            var res = await _categoryService.GetAsync(id);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryPostDto dto)
        {
            _logger.LogInformation("Category Created  by Admin");
            var res = await _categoryService.CreateAsync(dto);
            return StatusCode(res.StatusCode, res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Category Deleted  by Admin");
            var res = await _categoryService.RemoveAsync(id);
            return StatusCode(res.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            _logger.LogInformation("Category Updated  by Admin");
            var res = await _categoryService.UpdateAsync(id, dto);
            return StatusCode(res.StatusCode, res);
        }


    }
}
