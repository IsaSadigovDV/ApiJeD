using ApiFinal.Service.Dtos.Products;
using ApiFinal.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinal.App.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    [ApiExplorerSettings(GroupName ="admin_v1")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Product GetAll called by Admin");
            return StatusCode(200, await _productService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Product GetById called by Admin");
            var res = await _productService.GetAsync(id);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductPostDto dto)
        {
            _logger.LogInformation("Product Created by Admin");
            var res = await _productService.CreateAsync(dto);
            return StatusCode(res.StatusCode, res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Product Deleted by Admin");
            var res = await _productService.RemoveAsync(id);
            return StatusCode(res.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateDto dto)
        {
            _logger.LogInformation("Product Updated by Admin");
            var res = await _productService.UpdateAsync(id, dto);
            return StatusCode(res.StatusCode, res);
        }
    }
}
