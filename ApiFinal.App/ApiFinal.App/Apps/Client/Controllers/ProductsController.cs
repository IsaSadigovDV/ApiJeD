using ApiFinal.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinal.App.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "client_v1")]

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
