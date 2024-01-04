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
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Product GetAll called by client");
            return StatusCode(200, await _productService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Product GetById called by client");
            var res = await _productService.GetAsync(id);
            return StatusCode(res.StatusCode, res);
        }
    }
}
