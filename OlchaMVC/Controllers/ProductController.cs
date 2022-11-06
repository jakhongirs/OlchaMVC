using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OlchaMVC.Models;
using System.Text;

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

        /*ALL PRODUCTS*/
        public IActionResult Index()
        {
            List<ProductViewModel>modelList=new List<ProductViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/product").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
            }
            return View(modelList);
        }

        /*CREATE PRODUCT*/
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/product", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        /*UPDATE PRODUCT*/
        public IActionResult Edit(int id)
        {
            ProductViewModel model = new ProductViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/product/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/product/" + model.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create", model);
        }

        /*DELETE PRODUCT*/
        public IActionResult Delete(int id)
        {
            ProductViewModel model = new ProductViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/product/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(ProductViewModel model)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/product/" + model.Id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Delete", model);
        }

        /*DETAIL PRODUCT*/
        public IActionResult Details(int id)
        {
            ProductViewModel model = new ProductViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/product/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            return View(model);
        }
    }
}
