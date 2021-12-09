using Microsoft.AspNetCore.Mvc;
using Nasa.Apod.App.Models;
using Nasa.Apod.Business.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nasa.Apod.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApodService _apodService;
        public HomeController(IApodService apodService)
        {
            _apodService = apodService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _apodService.GetApodAsync();

            return View(result);
        }

        [Route("date/{date}")]
        public async Task<IActionResult> Index(string date)
        {
            var result = await _apodService.GetApodByDateAsync(date);

            return View(result);
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
