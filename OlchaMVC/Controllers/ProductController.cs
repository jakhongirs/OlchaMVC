using Microsoft.AspNetCore.Mvc;
using OlchaMVC.Models;

namespace OlchaMVC.Controllers
{
    public class ProductController : Controller
    {
        Uri baseAddress = new Uri("http://ec2-54-161-44-249.compute-1.amazonaws.com/api");
        HttpClient client;

        public ProductController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            List<ProductViewModel>modelList=new List<ProductViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/product").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
            }
            return View();
        }
    }
}
