using Microsoft.AspNetCore.Mvc;
using Nasa.App.Models;
using Nasa.Business.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nasa.App.Controllers
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

        public async Task<IActionResult> GetApodByDate(string date)
        {
            var result = await _apodService.GetApodByDateAsync(date);

            return View("Index", result);
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
