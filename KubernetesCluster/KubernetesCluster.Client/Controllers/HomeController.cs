using KubernetesCluster.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KubernetesCluster.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClientFactory.CreateClient("ShoppingAPIClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/product");
            _logger.LogInformation("api called --> kubernetesclusterapi");
            var content = await response.Content.ReadAsStringAsync();
            var productList = JsonConvert.DeserializeObject<IEnumerable<Product>>(content);
            
            return View(productList);
            //return View(new List<Product> { new Product { Name = "P!" }, new Product { Name = "P#" } });
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
