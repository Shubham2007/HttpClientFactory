using HttpClientFactoryDemo.Models;
using HttpClientFactoryDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HttpClientFactoryDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        /// <summary>
        /// Loads Posts on startup
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                string posts = await _homeService.GetPosts();
                return Content(posts);
            }
            catch (BrokenCircuitException bce)
            {
                return Content("Circuit is broken after 2 concesutive attempts" + Environment.NewLine + bce.Message);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
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
