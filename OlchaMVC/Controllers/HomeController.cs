using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OlchaMVC.Models;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace OlchaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string baseURL = "http://ec2-54-161-44-249.compute-1.amazonaws.com/api/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // Calling the web API and populating the data in view using DataTable
            DataTable dt = new DataTable();
            using(var client = new HttpClient())
            {
                client.BaseAddress=new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("product");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    dt=JsonConvert.DeserializeObject<DataTable>(results);
                    Console.WriteLine(results);
                }else
                {
                    Console.WriteLine("Error calling Web API");
                }

                ViewData.Model=dt;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}