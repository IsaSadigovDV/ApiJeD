using ApiFinal.Service.Dtos.Categories;
using ApiFinal.Service.Dtos.Products;
using ApiFinal.Service.Services.Implementations;
using ApiFinal.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinal.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return StatusCode(200, await _productService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _productService.GetAsync(id);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductPostDto dto)
        {
            var res = await _productService.CreateAsync(dto);
            return StatusCode(res.StatusCode, res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _productService.RemoveAsync(id);
            return StatusCode(res.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateDto dto)
        {
            var res = await _productService.UpdateAsync(id, dto);
            return StatusCode(res.StatusCode, res);
        }
    }
}
