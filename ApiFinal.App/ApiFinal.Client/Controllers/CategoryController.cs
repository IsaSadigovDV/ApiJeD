using ApiFinal.Client.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ApiFinal.Client.Controllers
{
    public class CategoryController : Controller
    {
        private readonly string EndoPoint = "https://localhost:44302";
        public async Task<IActionResult> Index()
        { 
            HttpClient httpClient = new HttpClient();
            GetItems<CategoryGetDto> getItems = new GetItems<CategoryGetDto>();
            getItems.Items = new List<CategoryGetDto>();


            var json = await httpClient.GetStringAsync(EndoPoint+ "/api/Categories");

            getItems = JsonConvert.DeserializeObject<GetItems<CategoryGetDto>>(json);

            return View(getItems.Items);
        }
    }
}
