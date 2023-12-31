using ApiFinal.Service.Dtos.Categories;
using ApiFinal.Service.Dtos.Products;
using ApiFinal.Service.Services.Implementations;
using ApiFinal.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinal.App.Client.Controllers
{
    [Route("api/[controller]")]
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
    }
}
